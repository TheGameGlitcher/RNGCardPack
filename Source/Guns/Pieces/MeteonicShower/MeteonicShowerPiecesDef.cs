using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.Pieces.Template;

namespace RNGCardPack.Source.Guns.Pieces.MeteonicShower
{
    public sealed class MeteonicShowerPiece1Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777004);

            config.Projectile = "DanmaStar2";

            config.Type = false;

            config.ShootType = 0;

            config.LastWave = true;

            config.ZeroHitNotDie = false;

            config.Scale = new float[][] { new float[] { 2.6f } };

            config.Color = new int[][] { new int[] { (int)BulletColor.RandomHue } };

            config.RootType = 0;

            config.Aim = 0;

            config.StartTime = 0;

            config.GInterval = 6;

            config.Group = 8;

            config.Way = new int[][] { new int[] { 1 } };

            config.GAngle = new float[][] { new float[] { 0f, 16f } };

            config.Range = new float[][] { new float[] { 360f } };

            config.StartSpeed = new float[][] { new float[] { 10f } };
            
            return config;
        }
    }
}
