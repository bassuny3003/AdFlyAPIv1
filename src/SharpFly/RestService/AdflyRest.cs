using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

using SharpFly.Config;

using DSoft.RestService.Events;
using DSoftRest = DSoft.RestService.RestService;
using DSoftUtil = DSoft.RestService.Utils.WebUtility;

namespace SharpFly.RestService
{
  internal class AdflyRest : DSoftRest
  {
    public Configuration Configuration { get; internal set; }

    public AdflyRest(Configuration config)
      : base()
    => Configuration = config;

    public AdflyRest(Configuration config, string uri)
      : base(uri)
    => Configuration = config;

    public AdflyRest Prepare(AuthType type = AuthType.BASIC)
    {
      this.AddParam("_user_id", Configuration.User_Id);
      this.AddParam("_api_key", Configuration.Public_Key);

      if(type == AuthType.BASIC)
      { }
      else
      {
        this.AddParam("_timestamp", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
        this.AddParam("_hash", computeHash());
      }

      return this;
    }

    private string computeHash()
    {
      string query = DSoftUtil.GenerateSortedParams(this.Params);

      var encoding = new System.Text.UTF8Encoding();
      byte[] key = encoding.GetBytes(Configuration.Secret_Key);

      HMACSHA256 sha256 = new HMACSHA256(key);

      byte[] digest = sha256.ComputeHash(Encoding.UTF8.GetBytes(query));
      StringBuilder builder = new StringBuilder();

      foreach (byte b in digest)
        builder.Append(b.ToString("x2"));

      return builder.ToString();
    }
  }
}
