using BagAPI.Data;

namespace BagAPI
{
    public class BagEndPoints
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/getdata", BagHandler.GetData);
            app.MapPost("/postdata", BagHandler.PostData);
            app.MapPut("/updatedata/{id}", BagHandler.UpdateData);
            app.MapGet("/getdatabyid", BagHandler.GetById);
            app.MapPost("/getdataByIdAndAge", BagHandler.GetByIdAndAge);
            app.MapPost("/putdata", BagHandler.PutData);
            app.MapPost("/patchdata", BagHandler.PatchData);
            app.MapPost("/Login", BagHandler.PostData);
        }
    }
}
