using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.IO;
using System;

namespace koinfast.Controllers
{
  public class BaseApiController : ApiController
  {
    public readonly IMapper _mapper;
    public readonly ApplicationDbContext _db;

    public BaseApiController()
    {
      _db = new ApplicationDbContext();
      _mapper = MapperConfig.Config().CreateMapper();
    }

    public bool SaveProofToFolder(HttpPostedFileBase i, string filepath)
    {

      if (i != null && i.ContentLength > 0)
      {
        var folderPath = filepath.Substring(0, filepath.LastIndexOf("/"));
        var newFileName = filepath.Substring(filepath.LastIndexOf("/") + 1);
        if (Directory.Exists(folderPath) == false)
        {
          Directory.CreateDirectory(HostingEnvironment.MapPath(folderPath));
          ModelState.AddModelError("", newFileName);
        }
        var path = HostingEnvironment.MapPath($"{folderPath}/{newFileName}");
        i.SaveAs(path);
        return true;
      }
      return false;
    }
  }
}
