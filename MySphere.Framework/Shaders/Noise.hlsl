sampler2D InputSampler : register(s0);

float Time;
float Intensity;
float Scale;
float Speed;

float random(float2 p)
{
    p = frac(p * float2(123.34, 456.21));
    p += dot(p, p + 45.32);

    return frac(p.x * p.y);
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(InputSampler, uv);

    float2 noiseUV = uv * Scale;

    // Двигаем noise во времени
    noiseUV += Time * Speed;

    float noise = random(noiseUV);

    // 0..1 → -1..1
    noise = noise * 2.0 - 1.0;

    // Очень слабое изменение яркости
    color.rgb += noise * Intensity;

    return color;
}