using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MlModelForDevices_App
{
    public static class NamesFromDetect
    {
        public static List<string> GetNamesFromDetect(string path)
        {
          //var m = context
            List<string> names = new List<string>();
            var image = MLImage.CreateFromFile(path);
            MlModelForDevices.ModelInput sampleData = new MlModelForDevices.ModelInput()
            {
                Image = image,
            };          
            
            var predictionResult = MlModelForDevices.Predict(sampleData);

            if (predictionResult.PredictedBoundingBoxes == null)
            { 
                return new List<string>();
            }
            var i = predictionResult.PredictedLabel;

            var boxes =
                predictionResult.PredictedBoundingBoxes.Chunk(4)
                    .Select(x => new { XTop = x[0], YTop = x[1], XBottom = x[2], YBottom = x[3] })
                    .Zip(predictionResult.Score, (a, b) => new { Box = a, Score = b });

            return i.ToList();
        }
    }
}
