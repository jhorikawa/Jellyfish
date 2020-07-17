using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class SplitGeometryByTypeComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SplitGeometryByTypeComponent class.
        /// </summary>
        public SplitGeometryByTypeComponent()
          : base("SplitGeometryByType", "SplitGeometryByType",
              "Split geometry by type.",
              "Jellyfish", "Geometry")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometries", "G", "Geometry to split.", GH_ParamAccess.tree);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Points", "P", "Points.", GH_ParamAccess.tree);
            pManager.AddGeometryParameter("Curves", "C", "Curves.", GH_ParamAccess.tree);
            pManager.AddGeometryParameter("Surfaces", "S", "Surfaces.", GH_ParamAccess.tree);
            pManager.AddGeometryParameter("Breps", "B", "Breps", GH_ParamAccess.tree);
            pManager.AddGeometryParameter("Meshes", "M", "Meshes", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<IGH_GeometricGoo> tree;
            if (!DA.GetDataTree(0, out tree)) return;

            GH_Structure<IGH_GeometricGoo> pointTree = new GH_Structure<IGH_GeometricGoo>();
            GH_Structure<IGH_GeometricGoo> curveTree = new GH_Structure<IGH_GeometricGoo>();
            GH_Structure<IGH_GeometricGoo> srfTree = new GH_Structure<IGH_GeometricGoo>();
            GH_Structure<IGH_GeometricGoo> brepTree = new GH_Structure<IGH_GeometricGoo>();
            GH_Structure<IGH_GeometricGoo> meshTree = new GH_Structure<IGH_GeometricGoo>();

            for(int i=0; i<tree.Branches.Count; i++)
            {
                var path = tree.Paths[i];
                var branch = tree.Branches[i];

                for(int n=0; n < branch.Count; n++)
                {
                    var item = branch[n];

                    if(item is GH_Point)
                    {
                        pointTree.Append(item, path);
                    }else if(item is GH_Curve || item is GH_Line || item is GH_Arc || item is GH_Circle || item is GH_Rectangle)
                    {
                        curveTree.Append(item, path);
                    }else if(item is GH_Surface)
                    {
                        srfTree.Append(item, path);
                    }else if(item is GH_Brep || item is GH_Box)
                    {
                        brepTree.Append(item, path);
                    }else if(item is GH_Mesh)
                    {
                        meshTree.Append(item, path);
                    }
                }
            }

            DA.SetDataTree(0, pointTree);
            DA.SetDataTree(1, curveTree);
            DA.SetDataTree(2, srfTree);
            DA.SetDataTree(3, brepTree);
            DA.SetDataTree(4, meshTree);
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
            get { return new Guid("56a53a5e-3264-496b-8992-b74bbab8cd77"); }
        }
    }
}