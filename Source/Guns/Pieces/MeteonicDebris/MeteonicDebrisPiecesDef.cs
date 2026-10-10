using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.Pieces.Template;

namespace RNGCardPack.Source.Guns.Pieces.MeteonicDebris
{
    public sealed class MeteonicDebrisPiece1Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777006);

            config.Projectile = "DanmaStar2";

            config.Type = false;

            config.ShootType = 1;

            config.LastWave = true;

            config.ZeroHitNotDie = true;

            config.Scale = new float[][] { new float[] { 1f } };

            config.Color = new int[][] { new int[] { (int)BulletColor.RandomHue } };

            config.RootType = 1;

            config.Radius = new float[][] { new float[] { 6f } };

            config.RadiusA = new float[][] { new float[] { 180f, 5f } };

            config.Aim = 1;

            config.StartTime = 0;

            config.GInterval = 2;

            config.Group = 12;

            config.Way = new int[][] { new int[] { 1 } };

            config.GAngle = new float[][] { new float[] { 75f, 5f } };

            config.StartSpeed = new float[][] { new float[] { 28f }, new float[] { -2f } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 1 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 60 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 15f } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed } };

            return config;
        }
    }
}
