using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.Pieces.Template;

namespace RNGCardPack.Source.Guns.Pieces.HammerPlayer
{
    public sealed class HammerPlayerPiece1Main1Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777003);

            config.Projectile = "Hammer";

            config.Type = false;

            config.ShootType = 0;

            config.LastWave = true;

            config.ZeroHitNotDie = false;

            config.Color = new int[][] { new int[] { (int)BulletColor.Default } };

            config.RootType = 0;

            config.X = new float[][] { new float[] { 0f, -0.3f } };

            config.Y = new float[][] { new float[] { 0f, -0.3f } };

            config.Aim = 0;

            config.StartTime = 0;

            config.GInterval = 0;

            config.Group = 1;

            config.Way = new int[][] { new int[] { 1 } };

            config.GAngle = new float[][] { new float[] { 45f } };

            config.StartSpeed = new float[][] { new float[] { 3f } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } },
                                             new int[][] { new int[] { 30 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 60 } },
                                                new int[][] { new int[] { 500 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 12f } },
                                                new float[][] { new float[] { 8f } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed },
                                          new int[] { (int)BulletEventType.Homing } };

            return config;
        }
    }
}
