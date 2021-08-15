// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.DeviceCommands
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band
{
  internal static class DeviceCommands
  {
    internal const ushort IndexShift = 0;
    internal const ushort IndexBits = 7;
    internal const ushort IndexMask = 127;
    internal const ushort TXShift = 7;
    internal const ushort TXBits = 1;
    internal const ushort TXMask = 128;
    internal const ushort CategoryShift = 8;
    internal const ushort CategoryBits = 8;
    internal const ushort CategoryMask = 65280;
    internal static ushort CargoCoreModuleGetVersion = DeviceCommands.MakeCommand(Facility.LibraryJutil, TX.True, (byte) 1);
    internal static ushort CargoCoreModuleGetUniqueID = DeviceCommands.MakeCommand(Facility.LibraryJutil, TX.True, (byte) 2);
    internal static ushort CargoCoreModuleWhoAmI = DeviceCommands.MakeCommand(Facility.LibraryJutil, TX.True, (byte) 3);
    internal static ushort CargoCoreModuleGetLogVersion = DeviceCommands.MakeCommand(Facility.LibraryJutil, TX.True, (byte) 5);
    internal static ushort CargoCoreModuleGetApiVersion = DeviceCommands.MakeCommand(Facility.LibraryJutil, TX.True, (byte) 6);
    internal static ushort CargoCoreModuleSdkCheck = DeviceCommands.MakeCommand(Facility.LibraryJutil, TX.False, (byte) 7);
    internal static ushort CargoTimeGetUtcTime = DeviceCommands.MakeCommand(Facility.LibraryTime, TX.True, (byte) 0);
    internal static ushort CargoTimeSetUtcTime = DeviceCommands.MakeCommand(Facility.LibraryTime, TX.False, (byte) 1);
    internal static ushort CargoTimeGetLocalTime = DeviceCommands.MakeCommand(Facility.LibraryTime, TX.True, (byte) 2);
    internal static ushort CargoTimeSetTimeZoneFile = DeviceCommands.MakeCommand(Facility.LibraryTime, TX.False, (byte) 4);
    internal static ushort CargoTimeZoneFileGetVersion = DeviceCommands.MakeCommand(Facility.LibraryTime, TX.True, (byte) 6);
    internal static ushort CargoLoggerGetChunkData = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.True, (byte) 1);
    internal static ushort CargoLoggerEnableLogging = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.False, (byte) 3);
    internal static ushort CargoLoggerDisableLogging = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.False, (byte) 4);
    internal static ushort CargoLoggerGetChunkCounts = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.True, (byte) 9);
    internal static ushort CargoLoggerFlush = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.False, (byte) 13);
    internal static ushort CargoLoggerGetChunkRangeMetadata = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.True, (byte) 14);
    internal static ushort CargoLoggerGetChunkRangeData = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.True, (byte) 15);
    internal static ushort CargoLoggerDeleteChunkRange = DeviceCommands.MakeCommand(Facility.LibraryLogger, TX.False, (byte) 16);
    internal static ushort CargoProfileGetDataApp = DeviceCommands.MakeCommand(Facility.ModuleProfile, TX.True, (byte) 6);
    internal static ushort CargoProfileSetDataApp = DeviceCommands.MakeCommand(Facility.ModuleProfile, TX.False, (byte) 7);
    internal static ushort CargoProfileGetDataFW = DeviceCommands.MakeCommand(Facility.ModuleProfile, TX.True, (byte) 8);
    internal static ushort CargoProfileSetDataFW = DeviceCommands.MakeCommand(Facility.ModuleProfile, TX.False, (byte) 9);
    internal static ushort CargoRemoteSubscriptionSubscribe = DeviceCommands.MakeCommand(Facility.LibraryRemoteSubscription, TX.False, (byte) 0);
    internal static ushort CargoRemoteSubscriptionUnsubscribe = DeviceCommands.MakeCommand(Facility.LibraryRemoteSubscription, TX.False, (byte) 1);
    internal static ushort CargoRemoteSubscriptionGetDataLength = DeviceCommands.MakeCommand(Facility.LibraryRemoteSubscription, TX.True, (byte) 2);
    internal static ushort CargoRemoteSubscriptionGetData = DeviceCommands.MakeCommand(Facility.LibraryRemoteSubscription, TX.True, (byte) 3);
    internal static ushort CargoRemoteSubscriptionSubscribeId = DeviceCommands.MakeCommand(Facility.LibraryRemoteSubscription, TX.False, (byte) 7);
    internal static ushort CargoRemoteSubscriptionUnsubscribeId = DeviceCommands.MakeCommand(Facility.LibraryRemoteSubscription, TX.False, (byte) 8);
    internal static ushort CargoNotification = DeviceCommands.MakeCommand(Facility.ModuleNotification, TX.False, (byte) 0);
    internal static ushort CargoNotificationProtoBuf = DeviceCommands.MakeCommand(Facility.ModuleNotification, TX.False, (byte) 5);
    internal static ushort CargoDynamicAppRegisterApp = DeviceCommands.MakeCommand(Facility.ModuleFireballAppsManagement, TX.False, (byte) 0);
    internal static ushort CargoDynamicAppRemoveApp = DeviceCommands.MakeCommand(Facility.ModuleFireballAppsManagement, TX.False, (byte) 1);
    internal static ushort CargoDynamicAppRegisterAppIcons = DeviceCommands.MakeCommand(Facility.ModuleFireballAppsManagement, TX.False, (byte) 2);
    internal static ushort CargoDynamicAppSetAppTileIndex = DeviceCommands.MakeCommand(Facility.ModuleFireballAppsManagement, TX.False, (byte) 3);
    internal static ushort CargoDynamicAppSetAppBadgeTileIndex = DeviceCommands.MakeCommand(Facility.ModuleFireballAppsManagement, TX.False, (byte) 5);
    internal static ushort CargoDynamicAppSetAppNotificationTileIndex = DeviceCommands.MakeCommand(Facility.ModuleFireballAppsManagement, TX.False, (byte) 11);
    internal static ushort CargoDynamicPageLayoutSet = DeviceCommands.MakeCommand(Facility.ModuleFireballPageManagement, TX.False, (byte) 0);
    internal static ushort CargoDynamicPageLayoutRemove = DeviceCommands.MakeCommand(Facility.ModuleFireballPageManagement, TX.False, (byte) 1);
    internal static ushort CargoDynamicPageLayoutGet = DeviceCommands.MakeCommand(Facility.ModuleFireballPageManagement, TX.True, (byte) 2);
    internal static ushort CargoInstalledAppListGet = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 0);
    internal static ushort CargoInstalledAppListSet = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 1);
    internal static ushort CargoInstalledAppListStartStripSyncStart = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 2);
    internal static ushort CargoInstalledAppListStartStripSyncEnd = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 3);
    internal static ushort CargoInstalledAppListGetDefaults = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 4);
    internal static ushort CargoInstalledAppListSetTile = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 6);
    internal static ushort CargoInstalledAppListGetTile = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 7);
    internal static ushort CargoInstalledAppListGetSettingsMask = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 13);
    internal static ushort CargoInstalledAppListSetSettingsMask = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 14);
    internal static ushort CargoInstalledAppListEnableSetting = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 15);
    internal static ushort CargoInstalledAppListDisableSetting = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.False, (byte) 16);
    internal static ushort CargoInstalledAppListGetNoImages = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 18);
    internal static ushort CargoInstalledAppListGetDefaultsNoImages = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 19);
    internal static ushort CargoInstalledAppListGetMaxTileCount = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 21);
    internal static ushort CargoInstalledAppListGetMaxTileAllocatedCount = DeviceCommands.MakeCommand(Facility.ModuleInstalledAppList, TX.True, (byte) 22);
    internal static ushort CargoSystemSettingsOobeCompleteClear = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.False, (byte) 0);
    internal static ushort CargoSystemSettingsOobeCompleteSet = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.False, (byte) 1);
    internal static ushort CargoSystemSettingsFactoryReset = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.True, (byte) 7);
    internal static ushort CargoSystemSettingsGetTimeZone = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.True, (byte) 10);
    internal static ushort CargoSystemSettingsSetTimeZone = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.False, (byte) 11);
    internal static ushort CargoSystemSettingsSetEphemerisFile = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.False, (byte) 15);
    internal static ushort CargoSystemSettingsGetMeTileImageID = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.True, (byte) 18);
    internal static ushort CargoSystemSettingsOobeCompleteGet = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.True, (byte) 19);
    internal static ushort CargoSystemSettingsEnableDemoMode = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.False, (byte) 25);
    internal static ushort CargoSystemSettingsDisableDemoMode = DeviceCommands.MakeCommand(Facility.ModuleSystemSettings, TX.False, (byte) 26);
    internal static ushort CargoSRAMFWUpdateLoadData = DeviceCommands.MakeCommand(Facility.LibrarySRAMFWUpdate, TX.False, (byte) 0);
    internal static ushort CargoSRAMFWUpdateBootIntoUpdateMode = DeviceCommands.MakeCommand(Facility.LibrarySRAMFWUpdate, TX.False, (byte) 1);
    internal static ushort CargoSRAMFWUpdateValidateAssets = DeviceCommands.MakeCommand(Facility.LibrarySRAMFWUpdate, TX.True, (byte) 2);
    internal static ushort CargoEFlashRead = DeviceCommands.MakeCommand(Facility.DriverEFlash, TX.True, (byte) 1);
    internal static ushort CargoGpsIsEnabled = DeviceCommands.MakeCommand(Facility.LibraryGps, TX.True, (byte) 6);
    internal static ushort CargoGpsEphemerisCoverageDates = DeviceCommands.MakeCommand(Facility.LibraryGps, TX.True, (byte) 13);
    internal static ushort CargoFireballUINavigateToScreen = DeviceCommands.MakeCommand(Facility.ModuleFireballUI, TX.False, (byte) 0);
    internal static ushort CargoFireballUIClearMeTileImage = DeviceCommands.MakeCommand(Facility.ModuleFireballUI, TX.False, (byte) 6);
    internal static ushort CargoFireballUISetSmsResponse = DeviceCommands.MakeCommand(Facility.ModuleFireballUI, TX.False, (byte) 7);
    internal static ushort CargoFireballUIGetAllSmsResponse = DeviceCommands.MakeCommand(Facility.ModuleFireballUI, TX.True, (byte) 11);
    internal static ushort CargoFireballUIReadMeTileImage = DeviceCommands.MakeCommand(Facility.ModuleFireballUI, TX.True, (byte) 14);
    internal static ushort CargoFireballUIWriteMeTileImageWithID = DeviceCommands.MakeCommand(Facility.ModuleFireballUI, TX.False, (byte) 17);
    internal static ushort CargoThemeColorSetFirstPartyTheme = DeviceCommands.MakeCommand(Facility.ModuleThemeColor, TX.False, (byte) 0);
    internal static ushort CargoThemeColorGetFirstPartyTheme = DeviceCommands.MakeCommand(Facility.ModuleThemeColor, TX.True, (byte) 1);
    internal static ushort CargoThemeColorSetCustomTheme = DeviceCommands.MakeCommand(Facility.ModuleThemeColor, TX.False, (byte) 2);
    internal static ushort CargoThemeColorReset = DeviceCommands.MakeCommand(Facility.ModuleThemeColor, TX.False, (byte) 4);
    internal static ushort CargoHapticPlayVibrationStream = DeviceCommands.MakeCommand(Facility.LibraryHaptic, TX.False, (byte) 0);
    internal static ushort CargoGoalTrackerSet = DeviceCommands.MakeCommand(Facility.ModuleGoalTracker, TX.False, (byte) 0);
    internal static ushort CargoFitnessPlansWriteFile = DeviceCommands.MakeCommand(Facility.LibraryFitnessPlans, TX.False, (byte) 4);
    internal static ushort CargoGolfCourseFileWrite = DeviceCommands.MakeCommand(Facility.LibraryGolf, TX.False, (byte) 0);
    internal static ushort CargoGolfCourseFileGetMaxSize = DeviceCommands.MakeCommand(Facility.LibraryGolf, TX.True, (byte) 1);
    internal static ushort CargoOobeSetStage = DeviceCommands.MakeCommand(Facility.ModuleOobe, TX.False, (byte) 0);
    internal static ushort CargoOobeGetStage = DeviceCommands.MakeCommand(Facility.ModuleOobe, TX.True, (byte) 1);
    internal static ushort CargoOobeFinalize = DeviceCommands.MakeCommand(Facility.ModuleOobe, TX.False, (byte) 2);
    internal static ushort CargoCortanaNotification = DeviceCommands.MakeCommand(Facility.ModuleCortana, TX.False, (byte) 0);
    internal static ushort CargoCortanaStart = DeviceCommands.MakeCommand(Facility.ModuleCortana, TX.False, (byte) 1);
    internal static ushort CargoCortanaStop = DeviceCommands.MakeCommand(Facility.ModuleCortana, TX.False, (byte) 2);
    internal static ushort CargoCortanaCancel = DeviceCommands.MakeCommand(Facility.ModuleCortana, TX.False, (byte) 3);
    internal static ushort CargoPersistedAppDataSetRunMetrics = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 0);
    internal static ushort CargoPersistedAppDataGetRunMetrics = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.True, (byte) 1);
    internal static ushort CargoPersistedAppDataSetBikeMetrics = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 2);
    internal static ushort CargoPersistedAppDataGetBikeMetrics = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.True, (byte) 3);
    internal static ushort CargoPersistedAppDataSetBikeSplitMult = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 4);
    internal static ushort CargoPersistedAppDataGetBikeSplitMult = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.True, (byte) 5);
    internal static ushort CargoPersistedAppDataSetWorkoutActivities = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 9);
    internal static ushort CargoPersistedAppDataGetWorkoutActivities = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.True, (byte) 16);
    internal static ushort CargoPersistedAppDataSetSleepNotification = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 17);
    internal static ushort CargoPersistedAppDataGetSleepNotification = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.True, (byte) 18);
    internal static ushort CargoPersistedAppDataDisableSleepNotification = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 19);
    internal static ushort CargoPersistedAppDataSetLightExposureNotification = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 21);
    internal static ushort CargoPersistedAppDataGetLightExposureNotification = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.True, (byte) 22);
    internal static ushort CargoPersistedAppDataDisableLightExposureNotification = DeviceCommands.MakeCommand(Facility.ModulePersistedApplicationData, TX.False, (byte) 23);
    internal static ushort CargoGetProductSerialNumber = DeviceCommands.MakeCommand(Facility.LibraryConfiguration, TX.True, (byte) 8);
    internal static ushort CargoKeyboardCmd = DeviceCommands.MakeCommand(Facility.LibraryKeyboard, TX.False, (byte) 0);
    internal static ushort CargoSubscriptionLoggerSubscribe = DeviceCommands.MakeCommand(Facility.ModuleLoggerSubscriptions, TX.False, (byte) 0);
    internal static ushort CargoSubscriptionLoggerUnsubscribe = DeviceCommands.MakeCommand(Facility.ModuleLoggerSubscriptions, TX.False, (byte) 1);
    internal static ushort CargoCrashDumpGetFileSize = DeviceCommands.MakeCommand(Facility.DriverCrashDump, TX.True, (byte) 1);
    internal static ushort CargoCrashDumpGetAndDeleteFile = DeviceCommands.MakeCommand(Facility.DriverCrashDump, TX.True, (byte) 2);
    internal static ushort CargoInstrumentationGetFileSize = DeviceCommands.MakeCommand(Facility.ModuleInstrumentation, TX.True, (byte) 4);
    internal static ushort CargoInstrumentationGetFile = DeviceCommands.MakeCommand(Facility.ModuleInstrumentation, TX.True, (byte) 5);
    internal static ushort CargoPersistedStatisticsRunGet = DeviceCommands.MakeCommand(Facility.ModulePersistedStatistics, TX.True, (byte) 2);
    internal static ushort CargoPersistedStatisticsWorkoutGet = DeviceCommands.MakeCommand(Facility.ModulePersistedStatistics, TX.True, (byte) 3);
    internal static ushort CargoPersistedStatisticsSleepGet = DeviceCommands.MakeCommand(Facility.ModulePersistedStatistics, TX.True, (byte) 4);
    internal static ushort CargoPersistedStatisticsGuidedWorkoutGet = DeviceCommands.MakeCommand(Facility.ModulePersistedStatistics, TX.True, (byte) 5);

    internal static ushort MakeCommand(Facility category, TX isTXCommand, byte index) => (ushort) ((uint) (ushort) ((uint) category << 8) | (uint) (ushort) ((uint) isTXCommand << 7) | (uint) (ushort) index);

    internal static void LookupCommand(
      ushort commandId,
      out Facility category,
      out TX isTXCommand,
      out byte index)
    {
      category = (Facility) (((int) commandId & 65280) >> 8);
      isTXCommand = (TX) (((int) commandId & 128) >> 7);
      index = (byte) ((uint) commandId & (uint) sbyte.MaxValue);
    }
  }
}
