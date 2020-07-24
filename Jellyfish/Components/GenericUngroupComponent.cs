﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Jellyfish.Data;

namespace Jellyfish.Components
{
    public class GenericUngroupComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GenericUngroupComponent class.
        /// </summary>
        public GenericUngroupComponent()
          : base("Generic Ungroup", "GenericUngroup",
              "Ungroup generic group data.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new GenericGroupParameter(), "Group", "G", "Generic group data to ungroup.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "Ungrouped data.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GenericGroup genericGroup = new GenericGroup();
            if (!DA.GetData(0, ref genericGroup)) return;

            DA.SetDataList(0, genericGroup.GetList());
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
            get { return new Guid("d5767476-b170-466b-acb5-4d22358f11b9"); }
        }
    }
}