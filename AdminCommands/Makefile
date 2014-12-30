
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = ../bin/Debug/mods/Server\ mods/AdminCommands.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Assembly-CSharp/bin/Debug/Assembly-CSharp.dll
BUILD_DIR = ../bin/Debug/mods/Server\ mods/

ADMINCOMMANDS_DLL_MDB_SOURCE=../bin/Debug/mods/Server\ mods/AdminCommands.dll.mdb
ADMINCOMMANDS_DLL_MDB=$(BUILD_DIR)/AdminCommands.dll.mdb
ASSEMBLY_CSHARP_DLL_SOURCE=../Assembly-CSharp/bin/Debug/Assembly-CSharp.dll
ASSEMBLY_CSHARP_FIRSTPASS_DLL_SOURCE=../libs/Assembly-CSharp-firstpass.dll
UNITYENGINE_DLL_SOURCE=../libs/UnityEngine.dll
ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL_SOURCE=../libs/Assembly-UnityScript-firstpass.dll
UNITYSCRIPT_LANG_DLL_SOURCE=../libs/UnityScript.Lang.dll
ASSEMBLY_CSHARP_DLL_MDB_SOURCE=../Assembly-CSharp/bin/Debug/Assembly-CSharp.dll.mdb
ASSEMBLY_CSHARP_DLL_MDB=$(BUILD_DIR)/Assembly-CSharp.dll.mdb

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = ../bin/Release/mods/Server\ mods/AdminCommands.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Assembly-CSharp/bin/Release/Assembly-CSharp.dll
BUILD_DIR = ../bin/Release/mods/Server\ mods/

ADMINCOMMANDS_DLL_MDB=
ASSEMBLY_CSHARP_DLL_SOURCE=../Assembly-CSharp/bin/Release/Assembly-CSharp.dll
ASSEMBLY_CSHARP_FIRSTPASS_DLL_SOURCE=../libs/Assembly-CSharp-firstpass.dll
UNITYENGINE_DLL_SOURCE=../libs/UnityEngine.dll
ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL_SOURCE=../libs/Assembly-UnityScript-firstpass.dll
UNITYSCRIPT_LANG_DLL_SOURCE=../libs/UnityScript.Lang.dll
ASSEMBLY_CSHARP_DLL_MDB=

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(ADMINCOMMANDS_DLL_MDB) \
	$(ASSEMBLY_CSHARP_DLL) \
	$(ASSEMBLY_CSHARP_FIRSTPASS_DLL) \
	$(UNITYENGINE_DLL) \
	$(ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL) \
	$(UNITYSCRIPT_LANG_DLL) \
	$(ASSEMBLY_CSHARP_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(ADMINCOMMANDS_PC)  


RESGEN=resgen2

ASSEMBLY_CSHARP_DLL = $(BUILD_DIR)/Assembly-CSharp.dll
ASSEMBLY_CSHARP_FIRSTPASS_DLL = $(BUILD_DIR)/Assembly-CSharp-firstpass.dll
UNITYENGINE_DLL = $(BUILD_DIR)/UnityEngine.dll
ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL = $(BUILD_DIR)/Assembly-UnityScript-firstpass.dll
UNITYSCRIPT_LANG_DLL = $(BUILD_DIR)/UnityScript.Lang.dll
ADMINCOMMANDS_PC = $(BUILD_DIR)/admincommands.pc

FILES =  \
	AdminCommands.cs \
	AssemblyInfo.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	admincommands.pc.in 

REFERENCES =  \
	../Assembly-CSharp/bin/Debug/Assembly-CSharp.dll \
	../libs/UnityEngine.dll \
	System \
	System.Core

DLL_REFERENCES =  \
	../libs/UnityEngine.dll

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,ASSEMBLY_CSHARP_DLL))
$(eval $(call emit-deploy-target,ASSEMBLY_CSHARP_FIRSTPASS_DLL))
$(eval $(call emit-deploy-target,UNITYENGINE_DLL))
$(eval $(call emit-deploy-target,ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL))
$(eval $(call emit-deploy-target,UNITYSCRIPT_LANG_DLL))
$(eval $(call emit-deploy-target,ASSEMBLY_CSHARP_DLL_MDB))
$(eval $(call emit-deploy-wrapper,ADMINCOMMANDS_PC,admincommands.pc))


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
	$(call cp,$(ADMINCOMMANDS_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_CSHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_CSHARP_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(UNITYENGINE_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(UNITYSCRIPT_LANG_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_CSHARP_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/pkgconfig'
	$(call cp,$(ADMINCOMMANDS_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ADMINCOMMANDS_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_CSHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_CSHARP_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(UNITYENGINE_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_UNITYSCRIPT_FIRSTPASS_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(UNITYSCRIPT_LANG_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_CSHARP_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ADMINCOMMANDS_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
