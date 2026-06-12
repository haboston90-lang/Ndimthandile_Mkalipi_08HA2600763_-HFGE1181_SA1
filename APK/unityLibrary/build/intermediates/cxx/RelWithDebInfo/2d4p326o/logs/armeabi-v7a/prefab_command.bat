@echo off
"C:\\Program Files\\Unity\\Hub\\Editor\\6000.0.63f1\\Editor\\Data\\PlaybackEngines\\AndroidPlayer\\OpenJDK\\bin\\java" ^
  --class-path ^
  "C:\\Users\\User\\.gradle\\caches\\modules-2\\files-2.1\\com.google.prefab\\cli\\2.1.0\\aa32fec809c44fa531f01dcfb739b5b3304d3050\\cli-2.1.0-all.jar" ^
  com.google.prefab.cli.AppKt ^
  --build-system ^
  cmake ^
  --platform ^
  android ^
  --abi ^
  armeabi-v7a ^
  --os-version ^
  34 ^
  --stl ^
  c++_shared ^
  --ndk-version ^
  27 ^
  --output ^
  "C:\\Users\\User\\AppData\\Local\\Temp\\agp-prefab-staging2040955294718477122\\staged-cli-output" ^
  "C:\\Users\\User\\.gradle\\caches\\8.13\\transforms\\6965e52cd47ead75a4eeb76e0c3cd8ed\\transformed\\jetified-games-frame-pacing-1.10.0\\prefab"
