namespace SocialMedia.Infraestructure.Options
{
    public class PasswordOption
    {

        public int SaltSize { get; set; }

        public int KeysSize { get; set; }


        public int Iterations {  get; set; }

    }
}
