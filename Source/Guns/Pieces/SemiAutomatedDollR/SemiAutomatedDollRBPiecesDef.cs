using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.Pieces.Template;

namespace RNGCardPack.Source.Guns.Pieces.SemiAutomatedDollR
{
    public sealed class SemiAutomatedDollRBPiece0Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777009);

            config.Projectile = "DanmaBigMagic";

            config.ShootType = 1;

            config.LastWave = false;

            config.Scale = new float[][] { new float[] { 0.8f } };

            config.Color = new int[][] { new int[] { (int)BulletColor.Grayscale } };

            config.RootType = 0;

            config.Aim = 1;

            config.StartTime = 0;

            config.GInterval = 0;

            config.Group = 2;

            config.Way = new int[][] { new int[] { 2 } };

            config.Life = new int[][] { new int[] { 35 } };

            config.Range = new float[][] { new float[] { 90f }, new float[] { 90f } };

            config.StartSpeed = new float[][] { new float[] { 15f }, new float[] { 10f } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 10 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 0f } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed, 1 } };

            return config;
        }
    }

    public sealed class SemiAutomatedDollRBPiece1Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777009, 1);

            config.Projectile = "PowerLaser";

            config.ShootType = 2;

            config.ParentPiece = 0;

            config.Type = true;

            config.LastWave = true;

            config.Scale = new float[][] { new float[] { 0.2f } };

            config.Color = new int[][] { new int[] { (int)BulletColor.Red } };

            config.Aim = 1;

            config.StartTime = 10;

            config.GInterval = 0;

            config.Group = 1;

            config.Way = new int[][] { new int[] { 1 } };

            config.Life = new int[][] { new int[] { 25 } };

            config.StartSpeed = new float[][] { new float[] { 0f } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 0f } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Homing } };

            return config;
        }
    }
}
