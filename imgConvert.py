from PIL import Image

# Daftar file gambar
image_files = ["path gambar"]

# Fungsi untuk mengubah DPI dan bit depth
def convert_image(file_name):
    with Image.open(file_name) as img:
        # Mengatur DPI ke 96x96
        img.save(file_name, dpi=(96, 96), format='JPEG', quality=95)

# Proses setiap file gambar
for image in image_files:
    convert_image(image)

print("Konversi selesai! Semua gambar telah diubah ke 96 DPI dan bit depth 24.")
