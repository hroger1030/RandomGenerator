# RandomGenerator

A C# library of random-number and random-value utilities for .NET — ranged numeric generation, weighted
random tables, dice-formula parsing/rolling, collection shuffling, and random string/color/date generation.
It favors a broad, explicit API surface (one method per type/range combination) over generics-heavy tricks,
so it's easy to discover what's available and to unit test each piece in isolation.

## Solution layout

| Project | Description |
|---|---|
| [RandomNumbers](RandomNumbers) | The library itself. Namespace: `RandomNumbers` (dice types live in the `RandomNumbers.Dice` sub-namespace). |
| [RandomNumbersTests](RandomNumbersTests) | NUnit test suite for the library. |

## File tree

Only the files that matter for using or extending the library are listed below; build output
(`bin`/`obj`) and IDE folders are omitted.

```
RandomGenerator/
├── RandomGenerator.sln
├── LICENSE.txt
├── CLAUDE.md
├── RandomNumbers/                     # Library project (namespace: RandomNumbers)
│   ├── RandomNumbers.csproj
│   ├── IRandomGenerator.cs            # Interface for RandomGenerator, for DI/testing
│   ├── RandomGenerator.cs             # Core random value generator
│   ├── CollectionRandomizer.cs        # Extension methods: ShuffleList, CryptoShuffleList
│   ├── IWeightedTable.cs
│   ├── WeightedTable.cs               # Weighted random selection table
│   ├── WordLists.cs                   # Sample word list used for text generation
│   └── Dice/
│       ├── DiceRoller.cs              # Rolls N-sided dice, with bonuses/multipliers
│       ├── DiceFormula.cs             # Parses/represents formulas like "3d6+2x10"
│       └── eDiceType.cs               # Enum of standard die sizes (D2..D10000)
└── RandomNumbersTests/                # NUnit test project (namespace: RandomNumbersTests)
    ├── RandomNumbersTests.csproj
    └── Objects/
        ├── DiceFormulaTests.cs
        ├── GenerateRandomTests.cs
        └── WeightedTableTests.cs
```

## Requirements

- .NET 10 SDK
- Windows (target platform)

## Building and testing

```
dotnet build
dotnet test
```

## Components

### RandomGenerator (`RandomNumbers/RandomGenerator.cs`)

Implements `IRandomGenerator` so it can be injected and mocked in consuming code. Wraps a single
`System.Random` instance (optionally seeded via the constructor) and exposes:

- **Basic types** — `Bool`, `Byte`, `Char`, `Double`, `Float`, `Int`, `Long`, `Short`, `ULong`,
  `ByteArray`, each with unbounded, `max`-only, and `min`/`max` overloads where it makes sense.
- **Math/geometry** — `UnitFloat`, `UnitDouble`, `Facing` (radians in `[0, 2π)`).
- **Strings & color** — `String`, `UnicodeString`, `Sentence`, `TextContent` (uses a supplied word
  list, see `WordLists`), `RGBColorString`, `RGBAColorString`, `ColorString` (color jittered around
  an input value).
- **Collections & objects** — `CollectionValue`/`DictionaryValue` (pick a random element, optionally
  removing it), `EnumValue<T>`, `Object<T>` (populates a new instance's writable properties with
  random values via reflection).
- **Non-uniform distributions** — `GaussianNormalDistribution` (Box–Muller), `NormallyDistributedDouble`/
  `Float`/`Int` (Irwin-Hall approximation via repeated rolls), `UniformDistributedDouble`,
  `ExponentiallyDistributedDouble`.
- **Time** — `RandomTime`, `RandomDateTime` (unbounded or within a `min`/`max` range).

### CollectionRandomizer (`RandomNumbers/CollectionRandomizer.cs`)

Extension methods on `IList<T>`:

- `ShuffleList` — in-place Fisher–Yates shuffle using `Random.Shared`.
- `CryptoShuffleList` — same shuffle algorithm, but sourced from `RandomNumberGenerator` (crypto RNG)
  for higher-quality randomness.

### WeightedTable\<T\> (`RandomNumbers/WeightedTable.cs`, `IWeightedTable.cs`)

A weighted random-selection table: add entries with a relative weight, then call `SelectRandomItem`
to draw one proportionally to its weight (optionally removing it from the table). Also supports
`ConvertToPercentileTable`, which renders the table as percentile ranges scaled to a given
`eDiceType` (e.g. express weights as ranges on a d100).

### Dice (`RandomNumbers/Dice`)

- `eDiceType` — enum of standard die sizes: D2, D3, D4, D6, D8, D10, D12, D20, D30, D100, D1000, D10000.
- `DiceRoller` — rolls one or more dice (by side count or `eDiceType`), with optional per-roll and
  final bonuses; also reports a die's statistical median via `GetDiceMedian`.
- `DiceFormula` — parses standard dice notation strings (`d6`, `2d4`, `3d6+2`, `4d4+2x10`) into a
  structured `Rolls`/`DiceType`/`Bonus`/`Multiplier` value, and renders back to that notation via
  `ToString()`.

### WordLists (`RandomNumbers/WordLists.cs`)

Static sample word data (`LatinWordList`, a "lorem ipsum" style list) for use with
`RandomGenerator.TextContent`.

## Code examples

### Basic random values

```csharp
using RandomNumbers;

IRandomGenerator rand = new RandomGenerator();

int roll = rand.Int(1, 100);          // [1, 100)
float unit = rand.UnitFloat();        // [0, 1]
bool coinFlip = rand.Bool();
string token = rand.String(12);       // 12 random alphanumeric characters
string hex = rand.RGBColorString();   // e.g. "#3F9A02"
```

### Dice

```csharp
using RandomNumbers.Dice;

var roller = new DiceRoller();
int damage = roller.Roll(3, eDiceType.D6, bonusPerRoll: 0, finalBonus: 2); // 3d6+2

var formula = new DiceFormula("2d4+1x10"); // parse "2d4+1, result x10"
Console.WriteLine(formula.ToString());     // "2D4+1x10"
```

### Weighted tables

```csharp
using RandomNumbers;

var lootTable = new WeightedTable<string>();
lootTable.AddEntry("common", 70f);
lootTable.AddEntry("rare", 25f);
lootTable.AddEntry("legendary", 5f);

string drop = lootTable.SelectRandomItem();
```

### Shuffling

```csharp
using RandomNumbers;

var deck = Enumerable.Range(1, 52).ToList();
deck.ShuffleList();       // fast, Random.Shared-backed shuffle
deck.CryptoShuffleList(); // crypto RNG-backed shuffle
```

## License

This project is licensed under the [MIT License](LICENSE.txt).

In short: yoiu can do anything you want with these files, short of removing the license or claiming them as your own. 
Go crazy with them.

See [LICENSE.txt](LICENSE.txt) for the full, legally-binding text.
