/*
// Create a new signal system
var signalSystem = new SignalSystem();

// Add some towers
signalSystem.AddTower(new Vector2(0, 0), TowerType.Main);  // Main tower at origin
signalSystem.AddTower(new Vector2(50, 50), TowerType.Relay);  // Relay tower at (50,50)

// Add an obstacle
signalSystem.AddObstacle(new Vector2(25, 0), new Vector2(25, 100));

// Check signal status at a position
Vector2 playerPos = new Vector2(30, 30);
SignalStatus status = signalSystem.GetSignalStatus(playerPos);

Console.WriteLine($"Signal Strength: {status.SignalStrength * 100}%");
Console.WriteLine($"Status: {status.Status}");
Console.WriteLine("Active Buffs: " + string.Join(", ", status.ActiveBuffs));
Console.WriteLine("Debuffs: " + string.Join(", ", status.Debuffs));
*/