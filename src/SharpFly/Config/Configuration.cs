namespace AdFlyAPIv1.Config
{
    public class Configuration
  {
    public string Public_Key  { get; internal set; }
    public string Secret_Key  { get; internal set; }
    public ulong  User_Id     { get; internal set; }

    public Configuration() { }

    public Configuration(string public_key, string secret_key, ulong uid)
    {
      this.Public_Key = public_key;
      this.Secret_Key = secret_key;
      this.User_Id    = uid;
    }
  }
}
