sampler2D input : register(s0);

// ============================================================
// Camera
// ============================================================

float2 CenterHi : register(c0);
float2 CenterLo : register(c1);

float ScaleHi : register(c2);
float ScaleLo : register(c3);

float Aspect : register(c4);

// ============================================================
// Colors
// ============================================================

float4 BackgroundColor : register(c5);
float4 EscapeColor     : register(c6);
float4 SetColor        : register(c7);


// ============================================================
// Double-Double helpers
//
// Representation:
//
//     value = hi + lo
//
// hi -> main value
// lo -> correction
// ============================================================


// ------------------------------------------------------------
// Quick Two Sum
// Requires |a| >= |b|
// ------------------------------------------------------------

float2 ddQuickTwoSum(float a, float b)
{
    float s = a + b;
    float e = b - (s - a);

    return float2(s, e);
}


// ------------------------------------------------------------
// Two Sum
// Exact for the sum of two floats
// ------------------------------------------------------------

float2 ddTwoSum(float a, float b)
{
    float s = a + b;

    float v = s - a;

    float e =
        (a - (s - v)) +
        (b - v);

    return float2(s, e);
}


// ------------------------------------------------------------
// Normalize DD number
// ------------------------------------------------------------

float2 ddNormalize(float2 a)
{
    return ddQuickTwoSum(a.x, a.y);
}


// ------------------------------------------------------------
// Addition
// ------------------------------------------------------------

float2 ddAdd(float2 a, float2 b)
{
    float2 s = ddTwoSum(a.x, b.x);

    float e =
        s.y +
        a.y +
        b.y;

    float2 r = ddQuickTwoSum(s.x, e);

    return r;
}


// ------------------------------------------------------------
// Subtraction
// ------------------------------------------------------------

float2 ddSub(float2 a, float2 b)
{
    float2 nb = float2(-b.x, -b.y);

    return ddAdd(a, nb);
}


// ------------------------------------------------------------
// Double-Double multiplication
//
// (ah + al) * (bh + bl)
//
// Uses Dekker splitting.
// ------------------------------------------------------------

float2 ddMul(float2 a, float2 b)
{
    const float SPLIT = 4097.0;

    float ah = a.x;
    float al = a.y;

    float bh = b.x;
    float bl = b.y;

    // --------------------------------------------------------
    // Split ah
    // --------------------------------------------------------

    float ca = SPLIT * ah;

    float ahHi = ca - (ca - ah);
    float ahLo = ah - ahHi;


    // --------------------------------------------------------
    // Split bh
    // --------------------------------------------------------

    float cb = SPLIT * bh;

    float bhHi = cb - (cb - bh);
    float bhLo = bh - bhHi;


    // --------------------------------------------------------
    // Main product
    // --------------------------------------------------------

    float p = ah * bh;


    // --------------------------------------------------------
    // Error of ah * bh
    // --------------------------------------------------------

    float e =
        ((ahHi * bhHi - p)
        + ahHi * bhLo
        + ahLo * bhHi)
        + ahLo * bhLo;


    // --------------------------------------------------------
    // Remaining DD terms
    // --------------------------------------------------------

    e += ah * bl;
    e += al * bh;
    e += al * bl;


    // --------------------------------------------------------
    // Renormalize
    // --------------------------------------------------------

    float2 r = ddQuickTwoSum(p, e);

    return r;
}


// ------------------------------------------------------------
// Double-Double × float
// ------------------------------------------------------------

float2 ddMulFloat(float2 a, float b)
{
    return ddMul(
        a,
        float2(b, 0.0)
    );
}


// ------------------------------------------------------------
// Double-Double square
// ------------------------------------------------------------

float2 ddSqr(float2 a)
{
    return ddMul(a, a);
}


// ============================================================
// Main
// ============================================================

float4 main(float2 uv : TEXCOORD) : COLOR
{
    // --------------------------------------------------------
    // UV -> NDC
    // --------------------------------------------------------

    float2 ndc = uv * 2.0 - 1.0;

    // WPF screen coordinates have Y downward.
    // Complex plane uses Y upward.
    ndc.y = -ndc.y;


    // ========================================================
    // Calculate C in Double-Double
    // ========================================================

    float2 centerX =
        float2(
            CenterHi.x,
            CenterLo.x
        );

    float2 centerY =
        float2(
            CenterHi.y,
            CenterLo.y
        );

    float2 scale =
        float2(
            ScaleHi,
            ScaleLo
        );


    // --------------------------------------------------------
    // Pixel offset
    // --------------------------------------------------------

    float offsetX =
        ndc.x * Aspect;

    float offsetY =
        ndc.y;


    // --------------------------------------------------------
    // c.real
    //
    // cr = CenterX + Scale * offsetX
    // --------------------------------------------------------

    float2 cr =
        ddAdd(
            centerX,
            ddMulFloat(
                scale,
                offsetX
            )
        );


    // --------------------------------------------------------
    // c.imag
    //
    // ci = CenterY + Scale * offsetY
    // --------------------------------------------------------

    float2 ci =
        ddAdd(
            centerY,
            ddMulFloat(
                scale,
                offsetY
            )
        );


    // ========================================================
    // z = 0
    // ========================================================

    float2 zr = float2(0.0, 0.0);
    float2 zi = float2(0.0, 0.0);


    // ========================================================
    // Mandelbrot
    // ========================================================

    const int MaxIterations = 254;

    float escaped = 0.0;
    float escapeIteration = (float)MaxIterations;


    [loop]
    for (int i = 0; i < MaxIterations; i++)
    {
        // ----------------------------------------------------
        // Once escaped, keep the result.
        //
        // No break!
        // ----------------------------------------------------

        if (escaped < 0.5)
        {
            // =================================================
            // zr²
            // =================================================

            float2 zr2 = ddSqr(zr);


            // =================================================
            // zi²
            // =================================================

            float2 zi2 = ddSqr(zi);


            // =================================================
            // zr * zi
            // =================================================

            float2 zrzi =
                ddMul(
                    zr,
                    zi
                );


            // =================================================
            // real:
            //
            // zr² - zi² + cr
            // =================================================

            float2 nextR =
                ddAdd(
                    ddSub(
                        zr2,
                        zi2
                    ),
                    cr
                );


            // =================================================
            // imaginary:
            //
            // 2 * zr * zi + ci
            // =================================================

            float2 twoZRZI =
                ddAdd(
                    zrzi,
                    zrzi
                );

            float2 nextI =
                ddAdd(
                    twoZRZI,
                    ci
                );


            // -------------------------------------------------
            // Store next z
            // -------------------------------------------------

            zr = nextR;
            zi = nextI;


            // =================================================
            // Escape test
            // =================================================

            float radius2 =
                zr.x * zr.x +
                zi.x * zi.x;


            if (radius2 > 4.0)
            {
                escaped = 1.0;
                escapeIteration = (float)i;
            }
        }
    }


    // ========================================================
    // Inside Mandelbrot set
    // ========================================================

    if (escaped < 0.5)
    {
        return SetColor;
    }


    // ========================================================
    // Smooth coloring
    // ========================================================

    float radius2 =
        zr.x * zr.x +
        zi.x * zi.x;

    float magnitude =
        sqrt(
            max(
                radius2,
                1.000001
            )
        );


    float smoothIteration =
        escapeIteration +
        1.0 -
        log(log(magnitude)) / log(2.0);


    float t =
        saturate(
            smoothIteration /
            (float)MaxIterations
        );


    return lerp(
        BackgroundColor,
        EscapeColor,
        t
    );
}