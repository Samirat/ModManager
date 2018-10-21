﻿using System.Reflection;
using Harmony;
using RimWorld;
using UnityEngine;
using Verse;

namespace ModManager
{
    public class ModManager: Mod
    {
        public ModManager( ModContentPack content ) : base( content )
        {
            Instance = this;
#if DEBUG
            HarmonyInstance.DEBUG = true;
#endif
            var harmonyInstance = HarmonyInstance.Create( "fluffy.modmanager" );
            harmonyInstance.PatchAll( Assembly.GetExecutingAssembly() );
        }

        public static ModManager Instance { get; private set; }

        public static void WriteAttributes()
        {
           Attributes.Write();
        }

        public static ModManagerSettings Attributes => Instance.GetSettings<ModManagerSettings>();
        public static ModManagerSettings Settings => Instance.GetSettings<ModManagerSettings>();

        public override string SettingsCategory() => I18n.SettingsCategory;
        public override void DoSettingsWindowContents( Rect canvas )
        {
            base.DoSettingsWindowContents( canvas );
            var listing = new Listing_Standard();
            listing.ColumnWidth = canvas.width;
            listing.Begin( canvas );
            listing.CheckboxLabeled( I18n.ShowPromotions, ref Settings.ShowPromotions, I18n.ShowPromotionsTip );

            if ( !Settings.ShowPromotions )
                GUI.color = Color.grey;

            listing.CheckboxLabeled( I18n.ShowPromotions_NotSubscribed, ref Settings.ShowPromotions_NotSubscribed );
            listing.CheckboxLabeled( I18n.ShowPromotions_NotActive, ref Settings.ShowPromotions_NotActive );

            GUI.color = Color.white;
            listing.End();
        }
    }
}