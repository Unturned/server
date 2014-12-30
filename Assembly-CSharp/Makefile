
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"
ASSEMBLY = bin/Debug/Assembly-CSharp.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug/

ASSEMBLY_CSHARP_DLL_MDB_SOURCE=bin/Debug/Assembly-CSharp.dll.mdb
ASSEMBLY_CSHARP_DLL_MDB=$(BUILD_DIR)/Assembly-CSharp.dll.mdb
ASSEMBLY_CSHARP_FIRSTPASS_DLL_SOURCE=../libs/Assembly-CSharp-firstpass.dll
UNITYENGINE_DLL_SOURCE=../libs/UnityEngine.dll
ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL_SOURCE=../libs/Assembly-UnityScript-firstpass.dll
UNITYSCRIPT_LANG_DLL_SOURCE=../libs/UnityScript.Lang.dll

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ "-define:TRACE"
ASSEMBLY = bin/Release/Assembly-CSharp.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/

ASSEMBLY_CSHARP_DLL_MDB=
ASSEMBLY_CSHARP_FIRSTPASS_DLL_SOURCE=../libs/Assembly-CSharp-firstpass.dll
UNITYENGINE_DLL_SOURCE=../libs/UnityEngine.dll
ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL_SOURCE=../libs/Assembly-UnityScript-firstpass.dll
UNITYSCRIPT_LANG_DLL_SOURCE=../libs/UnityScript.Lang.dll

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(ASSEMBLY_CSHARP_DLL_MDB) \
	$(ASSEMBLY_CSHARP_FIRSTPASS_DLL) \
	$(UNITYENGINE_DLL) \
	$(ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL) \
	$(UNITYSCRIPT_LANG_DLL)  

LINUX_PKGCONFIG = \
	$(ASSEMBLY_CSHARP_PC)  


RESGEN=resgen2

ASSEMBLY_CSHARP_FIRSTPASS_DLL = $(BUILD_DIR)/Assembly-CSharp-firstpass.dll
UNITYENGINE_DLL = $(BUILD_DIR)/UnityEngine.dll
ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL = $(BUILD_DIR)/Assembly-UnityScript-firstpass.dll
UNITYSCRIPT_LANG_DLL = $(BUILD_DIR)/UnityScript.Lang.dll
ASSEMBLY_CSHARP_PC = $(BUILD_DIR)/assembly-csharp.pc

FILES =  \
	Base.HUD/HUDCharacter.cs \
	Base.HUD/HUDCrafting.cs \
	Base.HUD/HUDDedicated.cs \
	Base.HUD/HUDEmote.cs \
	Base.HUD/HUDGame.cs \
	Base.HUD/HUDInteract.cs \
	Base.HUD/HUDInventory.cs \
	Base.HUD/HUDPause.cs \
	Base.HUD/HUDPlayers.cs \
	Base.HUD/HUDSkills.cs \
	Base.VoiceChat/UnityNetworkUSpeakSender.cs \
	Base.VoiceChat/USpeakCodecManager.cs \
	Base.VoiceChat/USpeaker.cs \
	Base.VoiceChat/USpeakOwnerInfo.cs \
	Base.VoiceChat/USpeakPlayer.cs \
	Base.VoiceChat/USpeakUtilities.cs \
	Base/ABridgeTooFar.cs \
	Base/ADayInTheSun.cs \
	Base/AI.cs \
	Base/AmmoStats.cs \
	Base/Animal.cs \
	Base/Animifier.cs \
	Base/ArmorStats.cs \
	Base/BagSize.cs \
	Base/BandMode.cs \
	Base/Barricade.cs \
	Base/BarricadesRegion.cs \
	Base/BarricadeStats.cs \
	Base/Bleed.cs \
	Base/Blueprint.cs \
	Base/Blueprints.cs \
	Base/Bumper.cs \
	Base/Chainsaw.cs \
	Base/Character.cs \
	Base/Chart.cs \
	Base/ChatType.cs \
	Base/Chess.cs \
	Base/ClientItem.cs \
	Base/Clothes.cs \
	Base/Colors.cs \
	Base/Consumeable.cs \
	Base/ConsumeableStats.cs \
	Base/Cooking.cs \
	Base/Coord2.cs \
	Base/DamageMultiplier.cs \
	Base/Dedicated.cs \
	Base/DefaultTalkController.cs \
	Base/DyeColor.cs \
	Base/Electricity.cs \
	Base/ElectricTrap.cs \
	Base/Epoch.cs \
	Base/Equipment.cs \
	Base/ExplosionTool.cs \
	Base/ExplosiveStats.cs \
	Base/FaceStyles.cs \
	Base/FancyAnimifier.cs \
	Base/Fertilizer.cs \
	Base/Flare.cs \
	Base/Flashlight.cs \
	Base/Footsteps.cs \
	Base/GameSettings.cs \
	Base/Generator.cs \
	Base/Gore.cs \
	Base/Graduate.cs \
	Base/GraphicsSettings.cs \
	Base/Ground.cs \
	Base/GunStats.cs \
	Base/HairColor.cs \
	Base/HairStyles.cs \
	Base/Harvest.cs \
	Base/HarvestStats.cs \
	Base/Images.cs \
	Base/InputSettings.cs \
	Base/Interact.cs \
	Base/Interactable.cs \
	Base/InteractionInterface.cs \
	Base/Inventory.cs \
	Base/Item.cs \
	Base/ItemAmount.cs \
	Base/ItemAnimations.cs \
	Base/ItemDescription.cs \
	Base/ItemEquipable.cs \
	Base/ItemName.cs \
	Base/Items/Backpack.cs \
	Base/Items/Bed.cs \
	Base/Items/Campfire.cs \
	Base/Items/Crate.cs \
	Base/Items/Door.cs \
	Base/Items/Gas.cs \
	Base/Items/Hat.cs \
	Base/Items/Weapons/Axe.cs \
	Base/Items/Weapons/Explosive.cs \
	Base/Items/Weapons/ExplosiveTrap.cs \
	Base/Items/Weapons/Grenade.cs \
	Base/Items/Weapons/Gun.cs \
	Base/Items/Weapons/Snare.cs \
	Base/ItemSounds.cs \
	Base/ItemsRegion.cs \
	Base/ItemStackable.cs \
	Base/ItemState.cs \
	Base/ItemType.cs \
	Base/ItemWeight.cs \
	Base/Jack.cs \
	Base/Lamp.cs \
	Base/Level.cs \
	Base/Life.cs \
	Base/LoadingScreen.cs \
	Base/LocalUSpeakSender.cs \
	Base/LockedCrate.cs \
	Base/Look.cs \
	Base/Loot.cs \
	Base/Maps.cs \
	Base/MasterSettings.cs \
	Base/MD5.cs \
	Base/MedicalStats.cs \
	Base/Medicine.cs \
	Base/Melee.cs \
	Base/MeleeStats.cs \
	Base/MenuCharacter.cs \
	Base/MenuConfigure.cs \
	Base/MenuConnect.cs \
	Base/MenuGraphics.cs \
	Base/MenuHost.cs \
	Base/MenuKeys.cs \
	Base/MenuPlay.cs \
	Base/MenuRegister.cs \
	Base/MenuServers.cs \
	Base/MenuSettings.cs \
	Base/MenuSingleplayer.cs \
	Base/MenuTitle.cs \
	Base/Movement.cs \
	Base/Network/NetworkBanned.cs \
	Base/Network/NetworkBans.cs \
	Base/Network/NetworkChat.cs \
	Base/Network/NetworkEffects.cs \
	Base/Network/NetworkErrorDelegate.cs \
	Base/Network/NetworkEventDelegate.cs \
	Base/Network/NetworkEvents.cs \
	Base/Network/NetworkHandler.cs \
	Base/Network/NetworkInterpolation.cs \
	Base/Network/NetworkLoader.cs \
	Base/Network/NetworkManager.cs \
	Base/Network/NetworkMasterServer.cs \
	Base/Network/NetworkPlayerDelegate.cs \
	Base/Network/NetworkRegions.cs \
	Base/Network/NetworkSounds.cs \
	Base/Network/NetworkTools.cs \
	Base/Network/NetworkUser.cs \
	Base/Network/NetworkUserList.cs \
	Base/Note.cs \
	Base/Ocean.cs \
	Base/Optic.cs \
	Base/Orient2.cs \
	Base/OwnerFinder.cs \
	Base/Packer.cs \
	Base/Painter.cs \
	Base/Pants.cs \
	Base/Passenger.cs \
	Base/Pick.cs \
	Base/Plant.cs \
	Base/Player.cs \
	Base/PlayerSettings.cs \
	Base/Point2.cs \
	Base/RayMasks.cs \
	Base/Refillable.cs \
	Base/Reflective.cs \
	Base/Reflector.cs \
	Base/Repair.cs \
	Base/Reputation.cs \
	Base/RudeFilter.cs \
	Base/Savedata.cs \
	Base/SendBehavior.cs \
	Base/Server.cs \
	Base/ServerBarricade.cs \
	Base/ServerItem.cs \
	Base/ServerSettings.cs \
	Base/ServerStructure.cs \
	Base/Shirt.cs \
	Base/Skill.cs \
	Base/Skills.cs \
	Base/SkinColor.cs \
	Base/SleekBox.cs \
	Base/SleekButton.cs \
	Base/SleekContainer.cs \
	Base/SleekDelegate.cs \
	Base/SleekDoc.cs \
	Base/SleekField.cs \
	Base/SleekFrame.cs \
	Base/SleekImage.cs \
	Base/SleekLabel.cs \
	Base/SleekRadio.cs \
	Base/SleekRender.cs \
	Base/SleekSlider.cs \
	Base/SleekWindow.cs \
	Base/Smoke.cs \
	Base/Sneaky.cs \
	Base/SpawnAnimals.cs \
	Base/SpawnBarricades.cs \
	Base/SpawnItems.cs \
	Base/SpawnPlayers.cs \
	Base/SpawnProjectiles.cs \
	Base/SpawnResources.cs \
	Base/SpawnStructures.cs \
	Base/SpawnVehicles.cs \
	Base/SpeakerMode.cs \
	Base/Stance.cs \
	Base/Steam.cs \
	Base/Structure.cs \
	Base/StructureStats.cs \
	Base/Sun.cs \
	Base/Texts.cs \
	Base/ThreeDMode.cs \
	Base/Throw.cs \
	Base/Trap.cs \
	Base/UnlockedDoor.cs \
	Base/Useable.cs \
	Base/Vehicle.cs \
	Base/Vest.cs \
	Base/Viewmodel.cs \
	Base/Voice.cs \
	Base/Zombie.cs \
	Database.cs \
	DataHolder.FileDatabase/FileDatabase.cs \
	Log/Logger.cs \
	ModLoader/BetterNetworkUser.cs \
	ModLoader/Command.cs \
	ModLoader/CommandHandlerMain.cs \
	ModLoader/CommandList.cs \
	ModLoader/Hook.cs \
	ModLoader/Ini.cs \
	ModLoader/Loader.cs \
	ModLoader/Reference.cs \
	ModLoader/UserList.cs \
	Properties/AssemblyInfo.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	ModLoader \
	Log \
	Base \
	DataHolder.FileDatabase \
	Base.HUD \
	Base.VoiceChat \
	GridManager \
	assembly-csharp.pc.in 

REFERENCES =  \
	../libs/Assembly-CSharp-firstpass.dll \
	../libs/Assembly-UnityScript-firstpass.dll \
	../libs/UnityEngine.dll \
	../libs/UnityScript.Lang.dll \
	System \
	System.Core

DLL_REFERENCES =  \
	../libs/Assembly-CSharp-firstpass.dll \
	../libs/Assembly-UnityScript-firstpass.dll \
	../libs/UnityEngine.dll \
	../libs/UnityScript.Lang.dll

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,ASSEMBLY_CSHARP_FIRSTPASS_DLL))
$(eval $(call emit-deploy-target,UNITYENGINE_DLL))
$(eval $(call emit-deploy-target,ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL))
$(eval $(call emit-deploy-target,UNITYSCRIPT_LANG_DLL))
$(eval $(call emit-deploy-wrapper,ASSEMBLY_CSHARP_PC,assembly-csharp.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_CSHARP_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_CSHARP_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(UNITYENGINE_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(UNITYSCRIPT_LANG_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/pkgconfig'
	$(call cp,$(ASSEMBLY_CSHARP_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_CSHARP_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_CSHARP_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(UNITYENGINE_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(UNITYSCRIPT_LANG_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_CSHARP_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
