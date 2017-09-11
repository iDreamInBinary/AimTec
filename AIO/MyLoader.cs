namespace SharpShooter_Loader
{
    #region

    using Aimtec.SDK.Events;

    using System;
    using System.IO;
    using System.Reflection;

    #endregion

    public static class MyLoader
    {
        public static void Main()
        {
            GameEvents.GameStart += () =>
            {
                var sharpshooter_loader = new Loader();
            };
        }

        private class Loader
        {
            public Loader()
            {
                var link = Environment.GetEnvironmentVariable("LocalAppData");
                var filePath = link + @"\AimtecLoader\Data\System\SharpShooter.dll";

                Console.WriteLine(filePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var prdll = Properties.Resources.SharpShooter;
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(prdll, 0, prdll.Length);
                }

                var a = Assembly.LoadFrom(filePath);
                var myType = a.GetType("SharpShooter.MyLoader");
                var main = myType.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

                main.Invoke(null, null);
            }
        }
    }
}
