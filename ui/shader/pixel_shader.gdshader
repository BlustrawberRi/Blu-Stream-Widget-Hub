shader_type canvas_item;

uniform int pixel_size : hint_range(1, 500, 1) = 2;

vec2 pixelize(vec2 uv, vec2 tex_size) {
	return (vec2(ivec2(uv/(tex_size*float(pixel_size)) )) *
	(tex_size*float(pixel_size)) );
}

void fragment() {
	vec2 uv = pixelize(UV, TEXTURE_PIXEL_SIZE);
	COLOR = texture(TEXTURE, uv);
}