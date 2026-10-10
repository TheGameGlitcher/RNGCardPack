using Cysharp.Threading.Tasks;
using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Config;
using System.Collections.Generic;
using UnityEngine;

namespace RNGCardPack.Source.Sounds
{
    public sealed class PaperMarioLuckySfx : SfxTemplate
    {
        public static DirectorySource sfx_dir = new DirectorySource(PInfo.GUID, "SFX");

        public override IdContainer GetId()
        {
            return RNGCardPackDefaultConfig.DefaultID(this);
        }

        public override SfxConfig MakeConfig()
        {
            SfxConfig config = DefaultConfig();

            config.Name = "PaperMarioLucky";

            config.Folder = "SFX";

            config.Path = "PaperMarioLucky.wav";

            config.Volume = 1;

            return config;
        }

        public override List<UniTask<AudioClip>> LoadSfxListAsync()
        {
            return new List<UniTask<AudioClip>>
            {
                ResourceLoader.LoadAudioClip("PaperMarioLucky" + ".wav", AudioType.WAV, sfx_dir)
            };
        }
    }
}
