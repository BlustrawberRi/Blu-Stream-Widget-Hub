shader_type canvas_item;

uniform float red_displacement = 0.0;
uniform float green_displacement = 0.0;
uniform float blue_displacement = 0.0;
uniform sampler2D offset_texture : hint_default_white;

void fragment( )
{
	vec4 texture_color = texture(TEXTURE, UV);
	vec4 color = texture_color;
	float red_dis = red_displacement * texture(offset_texture, UV).r;
	float green_dis = green_displacement * texture(offset_texture, UV).g;
	float blue_dis = blue_displacement * texture(offset_texture, UV).b;
	color.r = texture(TEXTURE, vec2(UV.x + red_dis, UV.y)).r;
	color.g = texture(TEXTURE, vec2(UV.x + green_dis, UV.y)).g;
	color.b = texture(TEXTURE, vec2(UV.x + blue_dis, UV.y)).b;

	COLOR = color;
}
