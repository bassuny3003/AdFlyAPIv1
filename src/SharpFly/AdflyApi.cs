using AdFlyAPIv1.Config;
using AdFlyAPIv1.RestService;
using System;
using System.Collections.Generic;

namespace AdFlyAPIv1
{
    public class AdflyApi
  {
    private string AdflyURL { get => "https://api.adf.ly"; }

    private Configuration configuration { get; set; }
    private AdflyRest rest_service;

    public AdflyApi(Configuration configuration)
    {
      this.configuration = configuration;
      this.rest_service = new AdflyRest(this.configuration);
    }

    public AdflyApi(string public_key, ulong user_id)
    {
        this.configuration = new Configuration()
        {
            Public_Key = public_key,
            User_Id = user_id
        };

        this.rest_service = new AdflyRest(this.configuration);
    }

    public AdflyApi(string public_key, string secret_key, ulong user_id)
    {
      this.configuration = new Configuration()
      {
        Public_Key = public_key,
        Secret_Key = secret_key,
        User_Id    = user_id
      };

      this.rest_service = new AdflyRest(this.configuration);
    }

    public string GetUserProfileDetails()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/profile";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }    

    public string GetUserAccountDetails()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/account";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }

    public string GetGroup()
    {
      return GetGroups(1);
    }

    public string GetGroups(int page)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/urlGroups";
      this.rest_service.ClearParams();

      this.rest_service.AddParam("_page", page);
      this.rest_service.Prepare(AuthType.HMAC);

      return rest_service.SendGetRequest();
    }
        
    public string CreateGroup(string GroupName)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/urlGroups";
      this.rest_service.ClearParams();

      this.rest_service.AddParam("name", GroupName);
      this.rest_service.Prepare(AuthType.HMAC);

      return rest_service.SendPostRequest();
    }
    
    public string GetDomains()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/domains";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }    

    public string GetAccountTotalReferrals(int includeBanned = 0)
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/accountTotalReferrals";
        this.rest_service.ClearParams();

        if (includeBanned != 0)
        {
            this.rest_service.AddParam("includeBanned", includeBanned);
        }

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }

    public string GetAccountCountries()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/accountCountries";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }    
      
    public string GetWithdrawalTransactions()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/withdrawalTransactions";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }

    public string GetWithdrawInformation()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/withdraw";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }
    public string RequestWithdraw()
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/requestWithdraw";
        this.rest_service.ClearParams();

        this.rest_service.Prepare(AuthType.HMAC);

        return this.rest_service.SendGetRequest();
    }

    public string Expand(string completeUrl)
    {
      return Expand(new string[] { completeUrl }, new string[] { });
    }

    public string Expand(string[] urls, string[] hashes)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/expand";
      this.rest_service.ClearParams();

      int index = 0;

      if(urls != null)
      {
        if(urls.Length > 1)
          foreach(string url in urls)
            this.rest_service.AddParam($"url[{index++}]", url);
        else if(urls.Length == 1)
         this.rest_service.AddParam("url", urls[0]);
      }

      index = 0;

      if(hashes != null)
      {
        if(hashes.Length > 1)
          foreach(string hash in hashes)
            this.rest_service.AddParam($"hash[{index++}]", hash);
        else if(hashes.Length == 1)
         this.rest_service.AddParam("hash", hashes[0]);
      }

      this.rest_service.Prepare();
      return this.rest_service.SendGetRequest();
    }

    public string Shorten(string url)
    {
      return Shorten(new string[] { url }, null, null, 0);
    }

    public string Shorten(string[] urls, string domain, string advertType, long groupId)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/shorten";
      this.rest_service.ClearParams();

      if (domain != null)
        this.rest_service.AddParam("domain", domain);

      if(advertType != null)
        this.rest_service.AddParam("advert_type", advertType);

      if(groupId != 0)
        this.rest_service.AddParam("group_id", groupId);

      int index = 0;
      foreach(string url in urls)
        this.rest_service.AddParam($"url[{index++}]", url);

      this.rest_service.Prepare();
      return this.rest_service.SendPostRequest();
    }

    public string Shorten(List<string> urls, string domain, string advertType, long groupId)
    {
        this.rest_service.Uri = $"{AdflyURL}/v1/shorten";
        this.rest_service.ClearParams();

        if (domain != null)
            this.rest_service.AddParam("domain", domain);

        if (advertType != null)
            this.rest_service.AddParam("advert_type", advertType);

        if (groupId != 0)
            this.rest_service.AddParam("group_id", groupId);

        int index = 0;
        foreach (string url in urls)
            this.rest_service.AddParam($"url[{index++}]", url);

        this.rest_service.Prepare();

        return this.rest_service.SendPostRequest();
    }

    public string GetUrls()
    {
      return GetUrls(1, "");
    }

    public string GetUrls(int page, String searchStr)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/urls";
      this.rest_service.ClearParams();

      this.rest_service.AddParam("_page", page).AddParam("q", searchStr);

      this.rest_service.Prepare(AuthType.HMAC);
      return this.rest_service.SendGetRequest();
    }

    public string UpdateUrl(long id, string url, string advertType, string title, long group_id, string fb_description, string fb_image)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/urls{id}";
      this.rest_service.ClearParams();

      if (url != null)
        this.rest_service.AddParam("url", url);

      if (advertType != null)
        this.rest_service.AddParam("advert_type", advertType);

      if (group_id != 0)
        this.rest_service.AddParam("group_id", group_id);

      if (title != null)
        this.rest_service.AddParam("title", title);

      if (fb_description != null)
        this.rest_service.AddParam("fb_description", fb_description);

      if (fb_image != null)
        this.rest_service.AddParam("fb_image", fb_image);

      this.rest_service.Prepare(AuthType.HMAC);
      return this.rest_service.SendPutRequest();
    }

    public string DeleteUrl(long id)
    {
      this.rest_service.Uri = $"{AdflyURL}/v1/urls/{id}";
      this.rest_service.ClearParams();

      this.rest_service.Prepare(AuthType.HMAC);
      return this.rest_service.SendDeleteRequest();
    }
  }
}
