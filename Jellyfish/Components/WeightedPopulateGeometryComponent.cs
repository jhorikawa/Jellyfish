using System;
using System.Collections.Generic;
using Rhino.Collections;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Jellyfish.Functions;

namespace Jellyfish.Components
{
    public class WeightedPopulateGeometryComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the WeightedPopulateGeometryComponent class.
        /// </summary>
        public WeightedPopulateGeometryComponent()
          : base("Weighted Populate Geometry", "WeightedPopulateGeometry",
              "Create weighted scattered points on geometry.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "Geometry to place points on.", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Number of Attractor Points", "N", "Number of attractor points.", GH_ParamAccess.item, 100);
            pManager.AddIntegerParameter("Iteration", "I", "Sampling iteration.", GH_ParamAccess.item, 100);
            pManager.AddPointParameter("Attractor Points", "A", "Attractor point list.", GH_ParamAccess.list);
            pManager.AddNumberParameter("Attractor Radiuses", "R", "Attractor radius list.", GH_ParamAccess.list, 100.0);
            pManager.AddNumberParameter("Attractor Weights", "W", "Attractor weight list.", GH_ParamAccess.list, 0.0);

            pManager[3].Optional = true;
            pManager[4].Optional = true;
            pManager[5].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "Scattered points.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_GeometricGoo shape = null;
            int num = -1;
            int ite = -1;
            List<Point3d> attractors = new List<Point3d>();
            List<double> radiuses = new List<double>();
            List<double> weights = new List<double>();

            if(!DA.GetData<IGH_GeometricGoo>(0, ref shape)) return;
            if(!DA.GetData(1, ref num)) return;
            if(!DA.GetData(2, ref ite)) return;
            DA.GetDataList(3, attractors);
            DA.GetDataList(4, radiuses);
            DA.GetDataList(5, weights);

            GeometryBase geo = null;
            if(shape is Mesh || shape is GH_Mesh || 
               shape is Brep || shape is GH_Brep ||
               shape is Surface || shape is GH_Surface ||
               shape is Curve || shape is GH_Curve ||
               shape is GH_Box)
            {
                geo = GH_Convert.ToGeometryBase(shape);
            }
            else
            {
                return;
            }


            var points = new Point3dList();
            var attracts = new Point3dList(attractors);
            var rnd = new Random();

            var bbox = geo.GetBoundingBox(true);

            for (int i = 0; i < num; i++)
            {

                if (points.Count == 0)
                {
                    var rndpt = CreateRandomPoint(rnd, geo, bbox);
                    points.Add(rndpt);
                }
                else
                {

                    double fdist = -1;
                    Point3d fpos = new Point3d();
                    for (int t = 0; t < Math.Max(Math.Min(ite, i), 10); t++)
                    {
                        var nrndpt = CreateRandomPoint(rnd, geo, bbox);

                        double nattractdist = 1;
                        for (int n = 0; n < attracts.Count; n++)
                        {
                            var nattract = attracts[n];
                            var rad = radiuses[Math.Min(n, radiuses.Count - 1)];
                            var pow = weights[Math.Min(n, radiuses.Count - 1)];

                            var ntdist = Math.Pow(JellyUtility.Remap(Math.Min(nattract.DistanceTo(nrndpt), rad), 0, rad, 0, 1.0), pow);
                            nattractdist *= ntdist;
                        }

                        var nindex = points.ClosestIndex(nrndpt);
                        var npos = points[nindex];

                        var ndist = npos.DistanceTo(nrndpt) * nattractdist;

                        if (fdist < ndist)
                        {
                            fdist = ndist;
                            fpos = nrndpt;
                        }
                    }
                    points.Add(fpos);
                }
            }


            DA.SetDataList(0, points);

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

        private Point3d CreateRandomPoint(Random rnd, GeometryBase geo, BoundingBox bbox)
        {
            var x = JellyUtility.Remap(rnd.NextDouble(), 0.0, 1.0, bbox.Min.X, bbox.Max.X);
            var y = JellyUtility.Remap(rnd.NextDouble(), 0.0, 1.0, bbox.Min.Y, bbox.Max.Y);
            var z = JellyUtility.Remap(rnd.NextDouble(), 0.0, 1.0, bbox.Min.Z, bbox.Max.Z);
            var pos = new Point3d(x, y, z);

            //return pos;
            if (geo.ObjectType == Rhino.DocObjects.ObjectType.Brep)
            {
                var brep = (Brep)geo;
                return brep.ClosestPoint(pos);
            }
            else if (geo.ObjectType == Rhino.DocObjects.ObjectType.Curve)
            {
                var curve = (Curve)geo;
                double t = 0;
                curve.ClosestPoint(pos, out t);
                return curve.PointAt(t);
            }
            else if (geo.ObjectType == Rhino.DocObjects.ObjectType.Mesh)
            {
                var mesh = (Mesh)geo;
                return mesh.ClosestPoint(pos);
            }
            else if (geo.ObjectType == Rhino.DocObjects.ObjectType.Surface)
            {
                var srf = (Surface)geo;
                double u, v;
                srf.ClosestPoint(pos, out u, out v);
                return srf.PointAt(u, v);
            }
            else
            {
                return Point3d.Origin;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("446ecfba-fdee-4c11-8642-b1c37b21350b"); }
        }
    }
}