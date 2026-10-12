"""
Genera GIFs animados a partir de las hojas de sprites de Cthulhu Jamboree.
Uso: python tools/make_gifs.py
Salida: docs/img/gifs/*.gif
"""
import os
from PIL import Image

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
ASSETS = os.path.join(ROOT, "PrototipoGameJam2025", "Assets")
OUT = os.path.join(ROOT, "docs", "img", "gifs")

SCALE = 3
DURATION = 80
TARGET_H = 280
BG = (16, 24, 32, 255)


def frames_from_dir(path, frame_w=None, frame_h=None):
    """Carga los PNG numerados de una carpeta como frames."""
    if not os.path.isdir(path):
        return []
    files = sorted(f for f in os.listdir(path) if f.lower().endswith(".png"))
    frames = []
    for f in files:
        im = Image.open(os.path.join(path, f)).convert("RGBA")
        if frame_w and frame_h:
            im = im.crop((0, 0, min(frame_w, im.width), min(frame_h, im.height)))
        frames.append(im)
    return frames


def save_gif(frames, name, target_h=TARGET_H, duration=DURATION):
    """Escala cada frame a una altura objetivo para que el GIF no pese demasiado."""
    if not frames:
        print(f"  (sin frames) {name}")
        return
    os.makedirs(OUT, exist_ok=True)
    flat = []
    for f in frames:
        ratio = target_h / f.height
        big = f.resize((max(1, int(f.width * ratio)), target_h), Image.LANCZOS)
        bg = Image.new("RGBA", big.size, BG)
        bg.alpha_composite(big)
        flat.append(bg.convert("P", palette=Image.ADAPTIVE, colors=255))
    out = os.path.join(OUT, name)
    flat[0].save(out, save_all=True, append_images=flat[1:],
                 duration=duration, loop=0, disposal=2)
    print(f"  {name}  ({len(flat)} frames, {flat[0].width}x{flat[0].height})")


def art(*parts):
    return os.path.join(ASSETS, "Art", *parts)


def main():
    print("Generando GIFs de Cthulhu Jamboree...")

    # --- Personajes ---
    save_gif(frames_from_dir(art("Characters", "Cool fish", "Idle")), "pez-cool-idle.gif")
    save_gif(frames_from_dir(art("Characters", "Cool fish", "Falling")), "pez-cool-caida.gif")
    save_gif(frames_from_dir(art("Characters", "Uncool Fish", "Idle")), "pez-uncool-idle.gif")
    save_gif(frames_from_dir(art("Characters", "Uncool Fish", "Falling")), "pez-uncool-caida.gif")

    # --- Burbujas ---
    save_gif(frames_from_dir(art("Bubble", "Idle")), "burbuja-idle.gif", duration=70)
    save_gif(frames_from_dir(art("Bubble", "Explosion")), "burbuja-explosion.gif", duration=70)

    # --- Burbujas del escenario ---
    save_gif(frames_from_dir(art("Scenary Bubbles", "Bubble 1")), "escenario-burbuja-1.gif")
    save_gif(frames_from_dir(art("Scenary Bubbles", "Bubble2")), "escenario-burbuja-2.gif")
    save_gif(frames_from_dir(art("Scenary Bubbles", "Buble 3")), "escenario-burbuja-3.gif")
    save_gif(frames_from_dir(art("Scenary Bubbles", "Exploding Bubble")), "escenario-explosion.gif")

    print("Listo.")


if __name__ == "__main__":
    main()
