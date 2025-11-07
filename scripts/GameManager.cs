using Godot;

public partial class GameManager : Node
{
    enum GameState {
		GAME_STARTING,
		GAME_OVER
    };

    enum RoundPhase
    {
        PLAYER_TURN,
        NPC_TURN,
        CHECK_WIN_CONDITIONS
    };



    private RoundPhase _currentPhase = new();

    [Export]
    PackedScene _playerScene, _npcScene;

    [Export]
    Marker3D _playerSpawnPos, _npcSpawnPos;

    [Export]
    Node3D _playersContainer;

    public Player Player;

    public NPC CurrentNPC;

    private bool _isPlayerTurn;

    public override void _Ready()
    {
        base._Ready();
        GameStart();
    }

    static void TestingFunction(Wave wave)
	{
        GD.PrintS($"Wave name: {wave.WaveType}");
    }

    private void GameStart()
    {
        // Spawn player
        // Spawn NPC (make this own function)

        // connect signals to player and NPC so that they can tell the GameManager that they have gone this round
        SpawnPlayer();
        SpawnNPC();

        var timer = GetTree().CreateTimer(1);
        timer.Timeout += () => ExecuteRoundPhase(RoundPhase.PLAYER_TURN);
    }

	private void GameOver()
	{

	}

	private void ExecuteRoundPhase(RoundPhase phase)
	{
		switch(phase)
		{
			case RoundPhase.PLAYER_TURN:
                _isPlayerTurn = true;
                HandlePlayerTurn(); break;
			case RoundPhase.NPC_TURN:
                _isPlayerTurn = false;
                HandleNPCTurn(); break;
			case RoundPhase.CHECK_WIN_CONDITIONS:
                CheckWinConditions(); break;
			default:
                GD.PrintErr("[GameManager.cs] \\ ExecuteRoundPhase(RoundPhase phase)] ERROR: INVALID ROUND PHASE."); break;
        }

        _currentPhase = phase;
    }

    private void HandlePlayerTurn()
	{
        Player.EnableInterface(true);

        ConnectTurnSignal(Player);
        ConnectTurnSignal(CurrentNPC, false);
    }

	private void HandleNPCTurn()
	{
        // Player.EnableInterface(false);

        ConnectTurnSignal(CurrentNPC);
        ConnectTurnSignal(Player, false);

        CurrentNPC.ChooseWave();
	}

    private void HandleRoundAction(Wave waveResource)
    {
        var currentPlayer = _isPlayerTurn ? "PLAYER" : "NPC";

        GD.PrintS($"[{currentPlayer}] Wave passed: {waveResource.WaveType}");
        ExecuteRoundPhase(RoundPhase.CHECK_WIN_CONDITIONS);
    }

	private void CheckWinConditions()
	{
        // Add a way to check if the player or the npc has quit/lost

        // Return to next state if game is still going.
        if (_isPlayerTurn)
            ExecuteRoundPhase(RoundPhase.NPC_TURN);
        else
            ExecuteRoundPhase(RoundPhase.PLAYER_TURN);

        // Add another check for round ended / player has won or lost.
    }

    private void SpawnPlayer()
    {
        Player player = _playerScene.Instantiate<Player>();
        // player.Connect(nameof(Player.TurnSignal), new Callable(this, nameof(TestingFunction)));
        _playersContainer.AddChild(player);
        player.GlobalPosition = _playerSpawnPos.GlobalPosition;
        Player = player;
    }

	private void SpawnNPC()
	{
        // No movement or anything right now, just bring the NPC into action.
        NPC npc = _npcScene.Instantiate<NPC>();
        // npc.Connect(nameof(Player.TurnSignal), new Callable(this, nameof(TestingFunction)));
        _playersContainer.AddChild(npc);
        npc.GlobalPosition = _npcSpawnPos.GlobalPosition;
        CurrentNPC = npc;
    }

	/// <summary>
    /// Connects and disconnects signals to prevent multiple signal cross-over
    /// </summary>
    /// <param name="player">Player can be either the actual player or the NPC</param>
	private void ConnectTurnSignal(Actionable player, bool disconnect = false)
	{
        if (player == null) { return; }

        // If we're disconnecting and the player doesnt have the signal connected, return
        // If we're connecting and the player has the signal connected, return
        if (disconnect && !player.IsConnected(Player.SignalName.TurnSignal, new Callable(this, nameof(HandleRoundAction))))
            return;

        if (!disconnect && player.IsConnected(Player.SignalName.TurnSignal, new Callable(this, nameof(HandleRoundAction))))
            return;

        if (disconnect)
		{
            player.Disconnect(nameof(player.TurnSignal), new Callable(this, nameof(HandleRoundAction)));
            return;
        }

        player.Connect(nameof(player.TurnSignal), new Callable(this, nameof(HandleRoundAction)));
    }
}
