# 🧩 C# 互動式拼圖遊戲 (Interactive Puzzle Game)

> 🚀 **以 C# Windows Forms 開發的桌面端休閒益智遊戲**，具備豐富的多媒體音效、多重關卡設計與完整的遊戲互動邏輯。

---

## 📖 專案簡介 (Project Overview)

本專案為一款基於 **C# Windows Forms** 開發的互動式拼圖遊戲，旨在展示對於「物件導向程式設計 (OOP)」、「事件驅動程式設計 (Event-Driven Programming)」以及「圖形化使用者介面 (GUI) 開發」的掌握能力。

玩家可以在遊戲中選擇不同的拼圖圖片進行挑戰，系統會自動打亂拼圖區塊，玩家透過點擊或拖曳來完成拼圖。遊戲過程中結合了背景音樂與過關音效，提供生動有趣的沉浸式體驗。此專案不僅具備完整的遊戲循環 (Game Loop)，也處理了複雜的狀態判斷與演算法實作。

---

## ✨ 核心功能與技術亮點 (Key Features)

### 1. 🎮 多重關卡與難度設計 (Multi-level Design)
* **動態視窗管理**：採用多個 Form (Form1 ~ Form12) 進行架構設計，每個視窗對應不同的遊戲選單、難度或特定圖片的拼圖關卡。
* **狀態維護**：在不同視窗之間無縫傳遞遊戲狀態（如：玩家分數、遊玩時間、通關紀錄）。

### 2. 🖼️ 核心拼圖演算法 (Puzzle Algorithm)
* **動態切割與洗牌**：將完整圖片 (解壓縮自 `pic.zip`) 動態切割成多個區塊，並使用隨機演算法 (Random Algorithm) 進行打亂，同時確保拼圖矩陣的「可解性 (Solvability)」。
* **勝負判定機制**：實作即時的座標或陣列比對邏輯，在玩家每一次移動後，精準判斷是否已達成拼圖完成的條件。

### 3. 🎵 多媒體與互動體驗 (Multimedia Integration)
* **音樂與音效控制**：整合系統音訊 API，在遊戲進行時播放背景音樂 (`music1.wav`)，並在玩家成功通關時播放歡呼音效 (`applause.wav`)，大幅提升遊戲體驗。
* **流暢的 UI/UX**：自訂視覺化的開始畫面 (`start.jpg`) 與背景 (`back.jpg`)，並處理滑鼠點擊 (Click) 與控制項焦點 (Focus) 等事件，打造直覺的互動介面。

---

## 🛠️ 技術架構 (Tech Stack)

* **開發語言**：C# 
* **應用程式框架**：Windows Forms (.NET Framework)
* **開發環境 (IDE)**：Visual Studio 
* **核心技術**：
  - Event-Driven Programming (事件驅動)
  - Windows API / 系統音效處理 (`System.Media.SoundPlayer`)
  - GDI+ 基礎圖形處理 (Bitmap, Image slicing)
  - 陣列與資料結構應用 (矩陣座標映射)

---

## 🚀 專案結構 (Project Structure)

```text
Windows-Programming/
├── 專題.sln                # Visual Studio 方案檔
├── pic.zip                 # 遊戲全域資源打包檔 (含所有拼圖圖片與音效，請於執行前解壓縮)
└── 專題/                   # 主程式專案目錄
    ├── Program.cs          # 程式進入點
    ├── Form1.cs            # 主選單/入口介面
    ├── Form2.cs ~ Form12.cs# 各關卡/功能視窗實作
    └── 專題.csproj         # C# 專案配置檔
```

---

## 💻 執行與測試 (How to Run)

1. 確認您的電腦已安裝 **Visual Studio** 以及 **.NET Desktop Development** 工作負載。
2. 將本專案 Clone 至本地：
   ```bash
   git clone https://github.com/renkai09/Windows-Programming.git
   ```
3. **重要：解壓縮資源檔**。請將根目錄下的 `pic.zip` 解壓縮，確保遊戲所需的圖片與音效檔放置於正確位置。
4. 使用 Visual Studio 打開專案根目錄下的 `專題.sln`。
5. 按下 `F5` 或點擊「開始」按鈕建置並執行專案。

---

## 🔮 學習心得與未來展望 (Future Work)

開發此專案讓我深刻體會到 C# 視窗程式開發的魅力，特別是在處理控制項的生命週期與記憶體管理時獲益良多。未來若有機會，預計會對此專案進行以下升級：
1. **成績排行榜系統**：導入本地資料庫 (如 SQLite) 或寫入檔案，紀錄玩家的最佳通關時間。
2. **自訂圖片匯入**：允許玩家從本機端上傳自己喜歡的圖片，由程式動態切割成拼圖。
3. **視圖架構重構**：將目前多個 Form 的設計，重構為 UserControl 的切換，或導入 WPF (MVVM 架構) 以達到更現代化的 UI 渲染效果。

---
*此專案為研究所推甄/專題展示作品，充分展現了在 C# 程式設計、GUI 開發邏輯及多媒體控制方面的實作能力。*
