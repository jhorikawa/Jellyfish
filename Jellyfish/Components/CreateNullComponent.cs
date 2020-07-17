using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class CreateNullComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CreateNullComponent class.
        /// </summary>
        public CreateNullComponent()
          : base("Create Null", "CreateNull",
              "Create null object.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Null Number", "N", "Number of null objects.", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Null Objects", "N", "Created null objects.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int num = 0;
            if (!DA.GetData(0, ref num)) return;


            List<object> nulls = new List<object>();
            for(int i =0; i<num; i++)
            {
                nulls.Add(null);
            }

            DA.SetDataList(0, nulls);
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
            get { return new Guid("6a8589ba-8ecf-41dd-8e10-e99d68b395c4"); }
        }
    }
}