using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class RemoveDuplicatePointsWithAssociatesComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RemoveDuplicatePointsWithAssociatesComponent class.
        /// </summary>
        public RemoveDuplicatePointsWithAssociatesComponent()
          : base("Remove Duplicate Points With Associates", "RemoveDuplicatePointsWithAssociates",
              "Remove duplicate points with associated objects.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "Point list to search duplicates.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Objects", "O", "Associated objects to be removed with points.", GH_ParamAccess.list);
            pManager.AddNumberParameter("Threshold", "T", "Threshold value to search duplicate points.", GH_ParamAccess.item, 0.01);

            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Points", "P", "Final ppoint list.", GH_ParamAccess.list);
            pManager.AddGenericParameter("Objects", "O", "Final associated object list", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Indexes", "I", "Final point index list", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Removed Indexes", "R", "Removed point list indexes.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> points = new List<Point3d>();
            List<object> objects = new List<object>();
            double t = 0;

            if (!DA.GetDataList(0, points)) return;
            DA.GetDataList(1, objects);
            if (!DA.GetData(2, ref t)) return;


            if(objects == null)
            {
                objects = new List<object>();
            }

            var outputPoints = new List<Point3d>();
            var outputObjects = new List<object>();
            var outputIndexes = new List<int>();
            var outputRemoveIndexes = new List<int>();
            for (int i = 0; i < points.Count; i++)
            {
                int Count = 0;

                for (int n = i + 1; n < points.Count; n++)
                {
                    if (n != i)
                    {
                        if (points[i].DistanceTo(points[n]) <= t)
                        {
                            Count = Count + 1;
                            break;
                        }
                    }
                }

                if (Count == 0)
                {
                    outputPoints.Add(points[i]);
                    outputIndexes.Add(i);
                    if(i < objects.Count)
                    {
                        outputObjects.Add(objects[i]);
                    }

                }
                else
                {
                    outputRemoveIndexes.Add(i);
                }
            }

            DA.SetDataList(0, outputPoints);
            DA.SetDataList(1, outputObjects);
            DA.SetDataList(2, outputIndexes);
            DA.SetDataList(3, outputRemoveIndexes);
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
            get { return new Guid("c60dcdb4-ac65-4046-9c72-ae803a11f816"); }
        }
    }
}