using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AntiGravity
{
	public class AntiGravPlayer : ModPlayer
	{
		// ran in Player.Update() right before checking forcedGravity & gravControl to set gravDir
		override public void PostUpdateRunSpeeds()
		{
			// invert forced gravity direction
			if (Player.forcedGravity > 0)
			{
				// setting these 3 causes the vanilla code to always set gravDir to 1 and not check the controls
				Player.forcedGravity = 0;
				Player.gravControl = false;
				Player.gravControl2 = false;
			}
			// if gravity isn't forced, check if we can control gravity, if not set it to -1
			else if (!(Player.gravControl || Player.gravControl2))
			{
				// this causes the vanilla code to always set gravDir to -1 and not check the controls
				// it also causes the player to die at the edge of space
				Player.forcedGravity = 1;
			}
			// else if gravity isn't forced and we can control it, allow vanilla code to run
			// it only changes gravDir if the player presses the input
		}

		// makes the death screen show upside down
		override public void UpdateDead()
		{
			Player.gravDir = -1f;
		}

		// gives the player the basic grappling hook as a starting item
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			return new[] {
				new Item(ItemID.GrapplingHook)
			};
		}
	}

	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class AntiGravity : Mod
	{

	}
}
