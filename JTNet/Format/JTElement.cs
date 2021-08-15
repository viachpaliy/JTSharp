using System;
using System.Collections.Generic;
using System.IO;
using JTNet.Types;
using JTNet.Format.Elements;

namespace JTNet.Format
{
  ///<summary>This class represents the basic JTElement parent to all the element types
  /// used in JT. This class dynamically looks up for element implementations and
  /// uses them to read specific element attributes. 
  ///</summary>
  public class JTElement {

 private static readonly String identifier_map = 
                "10dd1035-2ac8-11d1-9b-6b-00-80-c7-bb-59-97BaseNodeElement\n" + 
    		"10dd101b-2ac8-11d1-9b-6b-00-80-c7-bb-59-97GroupNodeElement\n" + 
    		"10dd102a-2ac8-11d1-9b-6b-00-80-c7-bb-59-97InstanceNodeElement\n" + 
    		"10dd102c-2ac8-11d1-9b-6b-00-80-c7-bb-59-97LODNodeElement\n" + 
    		"ce357245-38fb-11d1-a5-06-00-60-97-bd-c6-e1MetaDataNodeElement\n" + 
    		"d239e7b6-dd77-4289-a0-7d-b0-ee-79-f7-94-94NULLShapeNodeElement\n" + 
    		"ce357244-38fb-11d1-a5-06-00-60-97-bd-c6-e1PartNodeElement\n" + 
    		"10dd103e-2ac8-11d1-9b-6b-00-80-c7-bb-59-97PartitionNodeElement\n" + 
    		"10dd104c-2ac8-11d1-9b-6b-00-80-c7-bb-59-97RangeLODNodeElement\n" + 
    		"10dd10f3-2ac8-11d1-9b-6b-00-80-c7-bb-59-97SwitchNodeElement\n" + 
    		"10dd1059-2ac8-11d1-9b-6b-00-80-c7-bb-59-97BaseShapeNodeElement\n" + 
    		"98134716-0010-0818-19-98-08-00-09-83-5d-5aPointSetShapeNodeElement\n" + 
    		"10dd1048-2ac8-11d1-9b-6b-00-80-c7-bb-59-97PolygonSetShapeNodeElement\n" + 
    		"10dd1046-2ac8-11d1-9b-6b-00-80-c7-bb-59-97PolylineSetShapeNodeElement\n" + 
    		"e40373c1-1ad9-11d3-9d-af-00-a0-c9-c7-dd-c2PrimitiveSetShapeNodeElement\n" + 
    		"10dd1077-2ac8-11d1-9b-6b-00-80-c7-bb-59-97TriStripSetShapeNodeElement\n" + 
    		"10dd107f-2ac8-11d1-9b-6b-00-80-c7-bb-59-97VertexShapeNodeElement\n" + 
    		"4cc7a521-0728-11d3-9d-8b-00-a0-c9-c7-dd-c2WireHarnessSetShapeNode\n" + 
    		"10dd1001-2ac8-11d1-9b-6b-00-80-c7-bb-59-97BaseAttributeElement\n" + 
    		"10dd1014-2ac8-11d1-9b-6b-00-80-c7-bb-59-97DrawStyleAttributeElement\n" + 
    		"ad8dccc2-7a80-456d-b0-d5-dd-3a-0b-8d-21-e7FragmentShaderAttributeElement\n" + 
    		"10dd1083-2ac8-11d1-9b-6b-00-80-c7-bb-59-97GeometricTransformAttributeElement\n" + 
    		"10dd1028-2ac8-11d1-9b-6b-00-80-c7-bb-59-97InfiniteLightAttributeElement\n" + 
    		"10dd1096-2ac8-11d1-9b-6b-00-80-c7-bb-59-97LightSetAttributeElement\n" + 
    		"10dd10c4-2ac8-11d1-9b-6b-00-80-c7-bb-59-97LinestyleAttributeElement\n" + 
    		"10dd1030-2ac8-11d1-9b-6b-00-80-c7-bb-59-97MaterialAttributeElement\n" + 
    		"10dd1045-2ac8-11d1-9b-6b-00-80-c7-bb-59-97PointLightAttributeElement\n" + 
    		"8d57c010-e5cb-11d4-84-0e-00-a0-d2-18-2f-9dPointstyleAttributeElement\n" + 
    		"aa1b831d-6e47-4fee-a8-65-cd-7e-1f-2f-39-dbShaderEffectsAttributeElement\n" + 
    		"10dd1073-2ac8-11d1-9b-6b-00-80-c7-bb-59-97TextureImageAttributeElement\n" + 
    		"2798bcad-e409-47ad-bd-46-0b-37-1f-d7-5d-61VertexShaderAttributeElement\n" + 
    		"10dd104b-2ac8-11d1-9b-6b-00-80-c7-bb-59-97BasePropertyAtomElement\n" + 
    		"ce357246-38fb-11d1-a5-06-00-60-97-bd-c6-e1DatePropertyAtomElement\n" + 
    		"10dd102b-2ac8-11d1-9b-6b-00-80-c7-bb-59-97IntegerPropertyAtomElement\n" + 
    		"10dd1019-2ac8-11d1-9b-6b-00-80-c7-bb-59-97FloatingPointPropertyAtomElement\n" + 
    		"e0b05be5-fbbd-11d1-a3-a7-00-aa-00-d1-09-54LateLoadedPropertyAtomElement\n" + 
    		"10dd1004-2ac8-11d1-9b-6b-00-80-c7-bb-59-97JTObjectReferencePropertyAtom\n" + 
    		"10dd106e-2ac8-11d1-9b-6b-00-80-c7-bb-59-97StringPropertyAtomElement\n" + 
    		"873a70c0-2ac8-11d1-9b-6b-00-80-c7-bb-59-97JTBRepElement\n" + 
    		"ce357249-38fb-11d1-a5-06-00-60-97-bd-c6-e1PMIManagerMetaDataElement\n" + 
    		"ce357247-38fb-11d1-a5-06-00-60-97-bd-c6-e1PropertyProxyMetaDataElement\n" + 
    		"3e637aed-2a89-41f8-a9-fd-55-37-37-03-96-82NullShapeLODElement\n" + 
    		"98134716-0011-0818-19-98-08-00-09-83-5d-5aPointSetShapeLODElement\n" + 
    		"10dd109f-2ac8-11d1-9b-6b-00-80-c7-bb-59-97PolygonSetShapeLODElement\n" + 
    		"10dd10a1-2ac8-11d1-9b-6b-00-80-c7-bb-59-97PolylineSetShapeLODElement\n" + 
    		"e40373c2-1ad9-11d3-9d-af-00-a0-c9-c7-dd-c2PrimitiveSetShapeElement\n" + 
    		"10dd10ab-2ac8-11d1-9b-6b-00-80-c7-bb-59-97TriStripSetShapeLODElement\n" + 
    		"10dd10b0-2ac8-11d1-9b-6b-00-80-c7-bb-59-97VertexShapeLODElement\n" + 
    		"4cc7a523-0728-11d3-9d-8b-00-a0-c9-c7-dd-c2WireHarnessSetShapeElement\n" + 
    		"873a70e0-2ac9-11d1-9b-6b-00-80-c7-bb-59-97XTBRepElement\n" + 
    		"873a70d0-2ac8-11d1-9b-6b-00-80-c7-bb-59-97WireframeRepElement";

    public static readonly int HEADER_LENGTH = 4 + 1 + GUID.LENGTH; // 21

    ///Maps the existing, supported classed given the GUID
    static protected Dictionary<GUID, JTElement> classes = new Dictionary<GUID, JTElement>();
    
    ///Maps the unsupported classed given the GUID
    static protected Dictionary<GUID, String> unSupported = new Dictionary<GUID, String>();
    
    protected BinaryReader reader;

    protected int length;
    
    public GUID id;
    
    protected int objectBaseType;

    protected int bindingAttributes;

    protected JTElement() {}

    /// convenience method
    public static GUID idFor(String s) {
        return GUID.fromString(s);
    }
    
    


  }
}
