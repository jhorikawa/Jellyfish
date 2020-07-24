using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Jellyfish.Data;

namespace Jellyfish.Components
{
    public class GenericGroupComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GenericGroupComponent class.
        /// </summary>
        public GenericGroupComponent()
          : base("Generic Group", "GenericGroup",
              "Create a group for generic datas.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "Data list to create group.", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.RegisterParam(new GenericGroupParameter(), "Group", "G", "Generic Group", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<object> list = new List<object>();
            if (!DA.GetDataList(0, list)) return;

            GenericGroup genericGroup = new GenericGroup(list);

            DA.SetData(0, genericGroup);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("470329a7-0884-41fa-8175-782de466c3b5"); }
        }
    }
}