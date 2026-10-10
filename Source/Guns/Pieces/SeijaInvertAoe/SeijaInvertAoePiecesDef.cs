using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.Pieces.Template;

namespace RNGCardPack.Source.Guns.Pieces
{
    public sealed class SeijaInvertAoePiece1Main1Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777001);

            config.Projectile = "DanmaRice";

            config.ShootType = 1;

            config.LastWave = false;

            config.ZeroHitNotDie = true;

            config.Color = new int[][] { new int[] { (int)BulletColor.Red } };

            config.Aim = 0;

            config.GInterval = 2;

            config.Group = 15;

            config.Way = new int[][] { new int[] { 9 } };

            config.GAngle = new float[][] { new float[] { 0f }, new float[] { 2.7f } };

            config.Range = new float[][] { new float[] { 360f } };

            config.Life = new int[][] { new int[] { 60 } };

            config.StartTime = 0;

            config.StartSpeed = new float[][] { new float[] { 18 } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 60 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 0 } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed, 1 } };

            config.VanishV3 = new UnityEngine.Vector3(1f, 1f, 1f);

            return config;
        }
    }

    public sealed class SeijaInvertAoePiece1Sub1Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777001, 1);

            config.Projectile = "DanmaBlackRice";

            config.ShootType = 1;

            config.ParentPiece = 0;

            config.LastWave = true;

            config.ZeroHitNotDie = true;

            config.Color = new int[][] { new int[] { (int)BulletColor.Red } };

            config.GInterval = 2;

            config.Group = 15;

            config.Radius = new float[][] { new float[] { 9.5f } };

            config.RadiusA = new float[][] { new float[] { 45f } };

            config.Aim = 0;

            config.Way = new int[][] { new int[] { 9 } };

            config.GAngle = new float[][] { new float[] { 0f }, new float[] { 2.7f } };

            config.Range = new float[][] { new float[] { 360f } };

            config.StartTime = 60;

            config.StartSpeed = new float[][] { new float[] { -2 } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } },
                                             new int[][] { new int[] { 400 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 40 } },
                                                new int[][] { new int[] { 40 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { -10 } },
                                                new float[][] { new float[] { 20 } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed, 1 },
                                          new int[] { (int)BulletEventType.Speed, 1 } };

            config.LaunchSfx = "SeijaInvertBullet";

            config.HitBodySfx = "CommonHitBody";

            return config;
        }
    }

    public sealed class SeijaInvertAoePiece1Main2Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777001, 2);

            config.Projectile = "DanmaRice";

            config.ShootType = 1;

            config.LastWave = false;

            config.ZeroHitNotDie = true;

            config.Color = new int[][] { new int[] { (int)BulletColor.Blue } };

            config.Aim = 0;

            config.GInterval = 2;

            config.Group = 15;

            config.Way = new int[][] { new int[] { 9 } };

            config.GAngle = new float[][] { new float[] { 0f }, new float[] { -2.7f } };

            config.Range = new float[][] { new float[] { 360f } };

            config.Life = new int[][] { new int[] { 60 } };

            config.StartTime = 30;

            config.StartSpeed = new float[][] { new float[] { 18 } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 60 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 0 } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed, 1 } };

            config.VanishV3 = new UnityEngine.Vector3(1f, 1f, 1f);

            return config;
        }
    }

    public sealed class SeijaInvertAoePiece1Sub2Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777001, 3);

            config.Projectile = "DanmaBlackRice";

            config.ShootType = 1;

            config.ParentPiece = 2;

            config.LastWave = true;

            config.ZeroHitNotDie = true;

            config.Color = new int[][] { new int[] { (int)BulletColor.Blue } };

            config.GInterval = 2;

            config.Group = 15;

            config.Radius = new float[][] { new float[] { 9.5f } };

            config.RadiusA = new float[][] { new float[] { 45f } };

            config.Aim = 0;

            config.Way = new int[][] { new int[] { 9 } };

            config.GAngle = new float[][] { new float[] { 0f }, new float[] { -2.7f } };

            config.Range = new float[][] { new float[] { 360f } };

            config.StartTime = 90;

            config.StartSpeed = new float[][] { new float[] { -2 } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } },
                                             new int[][] { new int[] { 400 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 40 } },
                                                new int[][] { new int[] { 40 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { -10 } },
                                                new float[][] { new float[] { 20 } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed, 1 },
                                          new int[] { (int)BulletEventType.Speed, 1 } };

            config.LaunchSfx = "SeijaInvertBullet";

            config.HitBodySfx = "CommonHitBody";

            return config;
        }
    }
}
