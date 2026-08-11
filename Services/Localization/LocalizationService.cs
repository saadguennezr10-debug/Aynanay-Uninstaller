using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Windows;

namespace AynanayUninstaller.Services.Localization
{
    public class LocalizationService
    {
        private static readonly Lazy<LocalizationService> Instance = new(() => new LocalizationService());
        public static LocalizationService Instance => Instance.Value;

        private Dictionary<string, Dictionary<string, string>> _translations = new();
        private string _currentLanguage = "en";

        public event EventHandler? LanguageChanged;

        public LocalizationService()
        {
        }

        public void Initialize()
        {
            LoadLanguages();
            SetLanguage(GetSystemLanguage());
        }

        private void LoadLanguages()
        {
            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var langDir = Path.Combine(appDir, "Resources", "Languages");

            if (!Directory.Exists(langDir))
            {
                CreateDefaultLanguageFiles(langDir);
            }

            var langFiles = Directory.GetFiles(langDir, "*.json");
            foreach (var file in langFiles)
            {
                try
                {
                    var langCode = Path.GetFileNameWithoutExtension(file);
                    var json = File.ReadAllText(file);
                    var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
                    _translations[langCode] = translations;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading language file {file}: {ex.Message}");
                }
            }
        }

        private void CreateDefaultLanguageFiles(string langDir)
        {
            Directory.CreateDirectory(langDir);

            var enDict = new Dictionary<string, string>
            {
                { "app_title", "Aynanay Uninstaller" },
                { "app_description", "Modern Windows Uninstaller with Advanced Cleanup Features" },
                { "programs", "Programs" },
                { "installation_monitor", "Installation Monitor" },
                { "tools", "Tools" },
                { "settings", "Settings" },
                { "search", "Search installed programs..." },
                { "uninstall", "Uninstall" },
                { "forced_uninstall", "Forced Uninstall" },
                { "residual_scan", "Residual Scan" },
                { "safe_mode", "Safe" },
                { "moderate_mode", "Moderate" },
                { "advanced_mode", "Advanced" },
                { "start_scan", "Start Scan" },
                { "delete_selected", "Delete Selected" },
                { "cancel", "Cancel" },
                { "startup_manager", "Startup Manager" },
                { "junk_cleaner", "Junk Cleaner" },
                { "language", "Language" },
                { "about", "About" },
                { "version", "Version" },
                { "error", "Error" },
                { "warning", "Warning" },
                { "success", "Success" },
                { "loading", "Loading..." },
                { "no_programs", "No programs found" },
                { "file_size", "File Size" },
                { "publisher", "Publisher" },
                { "install_date", "Install Date" },
                { "install_location", "Install Location" },
            };

            var frDict = new Dictionary<string, string>
            {
                { "app_title", "Aynanay Désinstalleur" },
                { "app_description", "Désinstalleur Windows moderne avec fonctionnalités de nettoyage avancées" },
                { "programs", "Programmes" },
                { "installation_monitor", "Moniteur d'installation" },
                { "tools", "Outils" },
                { "settings", "Paramètres" },
                { "search", "Rechercher les programmes installés..." },
                { "uninstall", "Désinstaller" },
                { "forced_uninstall", "Désinstallation forcée" },
                { "residual_scan", "Analyse résiduelle" },
                { "safe_mode", "Sûr" },
                { "moderate_mode", "Modéré" },
                { "advanced_mode", "Avancé" },
                { "start_scan", "Démarrer l'analyse" },
                { "delete_selected", "Supprimer la sélection" },
                { "cancel", "Annuler" },
                { "startup_manager", "Gestionnaire de démarrage" },
                { "junk_cleaner", "Nettoyeur de fichiers inutiles" },
                { "language", "Langue" },
                { "about", "À propos" },
                { "version", "Version" },
                { "error", "Erreur" },
                { "warning", "Avertissement" },
                { "success", "Succès" },
                { "loading", "Chargement..." },
                { "no_programs", "Aucun programme trouvé" },
                { "file_size", "Taille du fichier" },
                { "publisher", "Éditeur" },
                { "install_date", "Date d'installation" },
                { "install_location", "Emplacement d'installation" },
            };

            var arDict = new Dictionary<string, string>
            {
                { "app_title", "Aynanay أداة إزالة البرامج" },
                { "app_description", "أداة إزالة برامج Windows حديثة بميزات تنظيف متقدمة" },
                { "programs", "البرامج" },
                { "installation_monitor", "مراقب التثبيت" },
                { "tools", "الأدوات" },
                { "settings", "الإعدادات" },
                { "search", "ابحث عن البرامج المثبتة..." },
                { "uninstall", "إزالة" },
                { "forced_uninstall", "إزالة قسرية" },
                { "residual_scan", "فحص البقايا" },
                { "safe_mode", "آمن" },
                { "moderate_mode", "معتدل" },
                { "advanced_mode", "متقدم" },
                { "start_scan", "ابدأ الفحص" },
                { "delete_selected", "حذف المحدد" },
                { "cancel", "إلغاء" },
                { "startup_manager", "مدير بدء التشغيل" },
                { "junk_cleaner", "منظف الملفات غير المهمة" },
                { "language", "اللغة" },
                { "about", "حول" },
                { "version", "الإصدار" },
                { "error", "خطأ" },
                { "warning", "تحذير" },
                { "success", "نجاح" },
                { "loading", "جاري التحميل..." },
                { "no_programs", "لم يتم العثور على برامج" },
                { "file_size", "حجم الملف" },
                { "publisher", "الناشر" },
                { "install_date", "تاريخ التثبيت" },
                { "install_location", "موقع التثبيت" },
            };

            var jaDict = new Dictionary<string, string>
            {
                { "app_title", "Aynanayアンインストーラー" },
                { "app_description", "高度なクリーンアップ機能を備えたモダンなWindowsアンインストーラー" },
                { "programs", "プログラム" },
                { "installation_monitor", "インストール監視" },
                { "tools", "ツール" },
                { "settings", "設定" },
                { "search", "インストール済みプログラムを検索..." },
                { "uninstall", "アンインストール" },
                { "forced_uninstall", "強制アンインストール" },
                { "residual_scan", "残留物スキャン" },
                { "safe_mode", "安全" },
                { "moderate_mode", "中程度" },
                { "advanced_mode", "詳細" },
                { "start_scan", "スキャン開始" },
                { "delete_selected", "選択した項目を削除" },
                { "cancel", "キャンセル" },
                { "startup_manager", "スタートアップマネージャー" },
                { "junk_cleaner", "ジャンククリーナー" },
                { "language", "言語" },
                { "about", "について" },
                { "version", "バージョン" },
                { "error", "エラー" },
                { "warning", "警告" },
                { "success", "成功" },
                { "loading", "読み込み中..." },
                { "no_programs", "プログラムが見つかりません" },
                { "file_size", "ファイルサイズ" },
                { "publisher", "発行者" },
                { "install_date", "インストール日" },
                { "install_location", "インストール場所" },
            };

            SaveLanguageFile(Path.Combine(langDir, "en.json"), enDict);
            SaveLanguageFile(Path.Combine(langDir, "fr.json"), frDict);
            SaveLanguageFile(Path.Combine(langDir, "ar.json"), arDict);
            SaveLanguageFile(Path.Combine(langDir, "ja.json"), jaDict);
        }

        private void SaveLanguageFile(string path, Dictionary<string, string> dict)
        {
            var json = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public void SetLanguage(string languageCode)
        {
            if (_translations.ContainsKey(languageCode))
            {
                _currentLanguage = languageCode;
                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string Translate(string key, string defaultValue = "")
        {
            if (_translations.TryGetValue(_currentLanguage, out var langDict))
            {
                if (langDict.TryGetValue(key, out var value))
                    return value;
            }

            if (_translations.TryGetValue("en", out var enDict))
            {
                if (enDict.TryGetValue(key, out var value))
                    return value;
            }

            return defaultValue;
        }

        public string CurrentLanguage => _currentLanguage;
        public List<string> AvailableLanguages => _translations.Keys.ToList();

        private string GetSystemLanguage()
        {
            var cultureName = CultureInfo.CurrentCulture.Name.Split('-')[0];
            return _translations.ContainsKey(cultureName) ? cultureName : "en";
        }
    }
}