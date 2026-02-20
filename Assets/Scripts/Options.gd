extends CanvasLayer

var startMenuScene = preload("res://scenes/StartMenu.tscn")
var musicBusIndex = AudioServer.get_bus_index("Music")

# Called when the node enters the scene tree for the first time.
func _ready():
	$VBoxContainer/BackButton.grab_focus()  # change keyboard focus to back btn

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_start_menu_button_pressed():
	get_tree().change_scene_to_file("res://scenes/StartMenu.tscn")

func _on_back_button_pressed():
	visible = !visible

func _on_h_slider_value_changed(value: float) -> void:
	var volume = linear_to_db($VBoxContainer/HBoxContainer/HSlider.value)
	AudioServer.set_bus_volume_db(musicBusIndex, volume)
