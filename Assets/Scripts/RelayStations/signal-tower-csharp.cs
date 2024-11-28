using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SignalTowerSystem
{
    public enum TowerType
    {
        Main,    // Strongest, longest range
        Relay,   // Good range, balanced
        Scanner, // Medium range, resource detection
        Defense  // Short range, enemy warning
    }

    public class TowerStats
    {
        public float Range { get; set; }
        public float Strength { get; set; }
        public string BuffType { get; set; }
        public Vector3 Color { get; set; } // RGB color for visualization

        public static Dictionary<TowerType, TowerStats> DefaultStats = new()
        {
            {TowerType.Main, new TowerStats 
                { Range = 100f, Strength = 1.0f, BuffType = "Surface Link", Color = new Vector3(0, 0, 1) }},
            {TowerType.Relay, new TowerStats 
                { Range = 80f, Strength = 0.8f, BuffType = "Signal Boost", Color = new Vector3(0, 1, 0) }},
            {TowerType.Scanner, new TowerStats 
                { Range = 60f, Strength = 0.6f, BuffType = "Resource Detection", Color = new Vector3(0.5f, 0, 1) }},
            {TowerType.Defense, new TowerStats 
                { Range = 40f, Strength = 0.5f, BuffType = "Enemy Warning", Color = new Vector3(1, 0, 0) }}
        };
    }

    public class SignalTower
    {
        public Guid Id { get; private set; }
        public Vector2 Position { get; set; }
        public TowerType Type { get; set; }
        public TowerStats Stats { get; private set; }

        public SignalTower(Vector2 position, TowerType type)
        {
            Id = Guid.NewGuid();
            Position = position;
            Type = type;
            Stats = TowerStats.DefaultStats[type];
        }
    }

    public class Obstacle
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }

        public Obstacle(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
    }

    public class SignalStatus
    {
        public float SignalStrength { get; set; }
        public List<string> ActiveBuffs { get; set; }
        public string Status { get; set; }
        public List<string> Debuffs { get; set; }

        public SignalStatus()
        {
            ActiveBuffs = new List<string>();
            Debuffs = new List<string>();
        }
    }

    public class SignalSystem
    {
        private List<SignalTower> towers;
        private List<Obstacle> obstacles;

        public SignalSystem()
        {
            towers = new List<SignalTower>();
            obstacles = new List<Obstacle>();
        }

        public bool CanPlaceTower(Vector2 position)
        {
            // First tower can be placed anywhere
            if (!towers.Any()) return true;

            // Check if position is in range of any existing tower
            return towers.Any(tower => 
                Vector2.Distance(position, tower.Position) <= tower.Stats.Range);
        }

        public bool AddTower(Vector2 position, TowerType type)
        {
            if (!CanPlaceTower(position))
                return false;

            towers.Add(new SignalTower(position, type));
            return true;
        }

        public void RemoveTower(Guid towerId)
        {
            towers.RemoveAll(t => t.Id == towerId);
        }

        public void AddObstacle(Vector2 start, Vector2 end)
        {
            obstacles.Add(new Obstacle(start, end));
        }

        private bool IsLineBlocked(Vector2 start, Vector2 end)
        {
            foreach (var obstacle in obstacles)
            {
                // Simple line segment intersection check
                Vector2 p = start;
                Vector2 r = end - start;
                Vector2 q = obstacle.Start;
                Vector2 s = obstacle.End - obstacle.Start;

                float cross = r.X * s.Y - r.Y * s.X;
                
                if (Math.Abs(cross) < float.Epsilon)
                    continue;

                float t = ((q.X - p.X) * s.Y - (q.Y - p.Y) * s.X) / cross;
                float u = ((q.X - p.X) * r.Y - (q.Y - p.Y) * r.X) / cross;

                if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                    return true;
            }
            return false;
        }

        private float CalculateInterference(SignalTower tower, Vector2 position)
        {
            return towers
                .Where(t => t.Id != tower.Id)
                .Sum(otherTower =>
                {
                    float distance = Vector2.Distance(tower.Position, otherTower.Position);
                    if (distance < tower.Stats.Range)
                        return 0.1f; // 10% interference per overlapping tower
                    return 0f;
                });
        }

        public SignalStatus GetSignalStatus(Vector2 position)
        {
            var status = new SignalStatus();
            float totalSignal = 0f;

            foreach (var tower in towers)
            {
                float distance = Vector2.Distance(position, tower.Position);
                
                // Skip if tower is out of range or blocked
                if (distance > tower.Stats.Range || IsLineBlocked(position, tower.Position))
                    continue;

                // Calculate signal contribution from this tower
                float interference = CalculateInterference(tower, position);
                float signalMultiplier = 1f - (distance / tower.Stats.Range);
                float towerSignal = Math.Max(0, tower.Stats.Strength * signalMultiplier - interference);
                
                totalSignal += towerSignal;

                // Add buff if signal is strong enough
                if (towerSignal > 0.2f) // 20% threshold for buff activation
                {
                    status.ActiveBuffs.Add(tower.Stats.BuffType);
                }
            }

            // Cap total signal at 100%
            status.SignalStrength = Math.Min(1f, totalSignal);

            // Determine status and debuffs
            if (status.SignalStrength < 0.3f)
            {
                status.Status = "CRITICAL - Lost contact with surface";
                status.Debuffs.AddRange(new[] { "Movement Speed -50%", "Resource Gathering -75%" });
            }
            else if (status.SignalStrength < 0.7f)
            {
                status.Status = "WARNING - Weak signal";
                status.Debuffs.Add("Movement Speed -25%");
            }
            else
            {
                status.Status = "STABLE - Good connection";
            }

            return status;
        }

        // Helper method to get all towers
        public IReadOnlyList<SignalTower> GetTowers() => towers.AsReadOnly();

        // Helper method to get all obstacles
        public IReadOnlyList<Obstacle> GetObstacles() => obstacles.AsReadOnly();


    }
}
