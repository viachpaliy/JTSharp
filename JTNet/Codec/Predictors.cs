using System;

namespace JTNet.Codec
{

  ///<summary> The primal input data is first run through a Predictor Type algorithm, 
  /// which produces an output array of residual values (i.e. difference from
  /// the predicted value), and this resulting output array of residual values 
  /// is the data which is actually the predicted value), and this resulting 
  /// output array of residual values is the data which is actually
  /// encoded/compressed.</summary>
  public class Predictors {
    	

    // ---------- Predictor Type Residual Unpacking ----------
    public enum PredictorType {
        Lag1, Lag2, Stride1, Stride2, StripIndex, Ramp, Xor1, Xor2, NULL
    }

    // Apply the predictor algorithm on the residual values to obtain the
    // primal values
    public static int[] unpackResiduals(int[] residuals, PredictorType predType) {

        int predicted;
        int len = residuals.Length;

        int[] primals = new int[len];

        for (int i = 0; i < len; i++)
            if (i < 4)
                // The first four values are just primers
                primals[i] = residuals[i];
            else {
                // Get a predicted value
                predicted = predictValue(primals, i, predType);

                if (predType == PredictorType.Xor1
                        || predType == PredictorType.Xor2)
                    // Decode the residual as the current value XOR predicted
                    primals[i] = residuals[i] ^ predicted;
                else
                    // Decode the residual as the current value plus predicted
                    primals[i] = residuals[i] + predicted;
            }
        return primals;
    }

    public static void unpackResidualsOverwrite(int[] residuals, PredictorType predType) {

        int predicted;
        int len = residuals.Length;

       // int[] primals = new int[len];

        for (int i = 0; i < len; i++)
            if (i < 4)
                // The first four values are just primers
                residuals[i] = residuals[i];
            else {
                // Get a predicted value
                predicted = predictValue(residuals, i, predType);

                if (predType == PredictorType.Xor1
                        || predType == PredictorType.Xor2)
                    // Decode the residual as the current value XOR predicted
                    residuals[i] = residuals[i] ^ predicted;
                else
                    // Decode the residual as the current value plus predicted
                    residuals[i] = residuals[i] + predicted;
            }
        return;
    }
    // Predict a value given the 4 previous values. Several algorithms may be
    // used depending on the data considered.
    public static int predictValue(int[] primals, int index,
            PredictorType predType) {
        int predicted;
        int v1 = primals[index - 1];
        int v2 = primals[index - 2];
        int v3 = primals[index - 3];
        int v4 = primals[index - 4];

        switch (predType) {
        default:
        case PredictorType.Lag1:
        case PredictorType.Xor1:
            predicted = v1;
            break;
        case PredictorType.Lag2:
        case PredictorType.Xor2:
            predicted = v2;
            break;
        case PredictorType.Stride1:
            predicted = v1 + (v1 - v2);
            break;
        case PredictorType.Stride2:
            predicted = v2 + (v2 - v4);
            break;
        case PredictorType.StripIndex:

            if (v2 - v4 < 8 && v2 - v4 > -8)
                predicted = v2 + (v2 - v4);
            else
                predicted = v2 + 2;
            break;
            
        case PredictorType.Ramp:
            predicted = index;
            break;
        }
        return predicted;
    }

  }
}
