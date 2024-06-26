

using MlModelForDevices_App;
using Microsoft.ML.Data;


var image = MLImage.CreateFromFile(@"C:\Users\user\Desktop\train\Multimeter_1_jpg.rf.01c49b53e2ed72d83ebc74faaf907d89.jpg");
MlModelForDevices.ModelInput sampleData = new MlModelForDevices.ModelInput()
{
    Image = image,
};

var predictionResult = MlModelForDevices.Predict(sampleData);

if (predictionResult.PredictedBoundingBoxes == null)
{

    return;
}
var boxes =
    predictionResult.PredictedBoundingBoxes.Chunk(4)
        .Select(x => new { XTop = x[0], YTop = x[1], XBottom = x[2], YBottom = x[3] })
        .Zip(predictionResult.Score, (a, b) => new { Box = a, Score = b });

foreach (var item in boxes)
{
  
}

