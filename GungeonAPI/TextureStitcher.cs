using System;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x02000012 RID: 18
	public static class TextureStitcher
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public static Rect AddFaceCardToAtlas(Texture2D tex, Texture2D atlas, int index, Rect bounds)
		{
			int num = (int)(bounds.width / 34f);
			int num2 = (int)(bounds.height / 34f);
			int num3 = index % num;
			int num4 = index / num;
			bool flag = num3 >= num || num4 >= num2;
			Rect result;
			if (flag)
			{
				Tools.PrintError<string>("Not enough room left on the Facecard Atlas for this facecard!", "FF0000");
				result = Rect.zero;
			}
			else
			{
				int num5 = (int)bounds.x + num3 * 34;
				int num6 = (int)bounds.y + num4 * 34;
				for (int i = 0; i < tex.width; i++)
				{
					for (int j = 0; j < tex.height; j++)
					{
						atlas.SetPixel(i + num5, j + num6, tex.GetPixel(i, j));
					}
				}
				atlas.Apply(false, false);
				result = new Rect((float)num5 / (float)atlas.width, (float)num6 / (float)atlas.height, 34f / (float)atlas.width, 34f / (float)atlas.height);
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006CFC File Offset: 0x00004EFC
		public static Rect ReplaceFaceCardInAtlas(Texture2D tex, Texture2D atlas, Rect region)
		{
			int num = (int)Mathf.Round((float)atlas.width * region.x);
			int num2 = (int)Mathf.Round((float)atlas.width * region.y);
			for (int i = 0; i < tex.width; i++)
			{
				for (int j = 0; j < tex.height; j++)
				{
					atlas.SetPixel(i + num, j + num2, tex.GetPixel(i, j));
				}
			}
			atlas.Apply(false, false);
			return new Rect((float)num / (float)atlas.width, (float)num2 / (float)atlas.height, 34f / (float)atlas.width, 34f / (float)atlas.height);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public static Texture2D CropWhiteSpace(this Texture2D orig)
		{
			Rect trimmedBounds = orig.GetTrimmedBounds();
			Texture2D texture2D = new Texture2D((int)trimmedBounds.width, (int)trimmedBounds.height);
			texture2D.name = orig.name;
			int num = (int)trimmedBounds.x;
			while ((float)num < trimmedBounds.x + trimmedBounds.width)
			{
				int num2 = (int)trimmedBounds.y;
				while ((float)num2 < trimmedBounds.y + trimmedBounds.height)
				{
					texture2D.SetPixel(num - (int)trimmedBounds.x, num2 - (int)trimmedBounds.y, orig.GetPixel(num, num2));
					num2++;
				}
				num++;
			}
			texture2D.Apply(false, false);
			return texture2D;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006E80 File Offset: 0x00005080
		public static Rect GetTrimmedBounds(this Texture2D t)
		{
			int num = t.width;
			int num2 = t.height;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < t.width; i++)
			{
				for (int j = 0; j < t.height; j++)
				{
					bool flag = t.GetPixel(i, j) != Color.clear;
					if (flag)
					{
						bool flag2 = i < num;
						if (flag2)
						{
							num = i;
						}
						bool flag3 = j < num2;
						if (flag3)
						{
							num2 = j;
						}
						bool flag4 = i > num3;
						if (flag4)
						{
							num3 = i;
						}
						bool flag5 = j > num4;
						if (flag5)
						{
							num4 = j;
						}
					}
				}
			}
			return new Rect((float)num, (float)num2, (float)(num3 - num + 1), (float)(num4 - num2 + 1));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006F4C File Offset: 0x0000514C
		public static Texture2D AddMargin(this Texture2D texture)
		{
			Texture2D texture2D = new Texture2D(texture.width + 2 * TextureStitcher.padding, texture.height + 2 * TextureStitcher.padding);
			texture2D.name = texture.name;
			texture2D.filterMode = texture.filterMode;
			for (int i = 0; i < texture2D.width; i++)
			{
				for (int j = 0; j < texture2D.height; j++)
				{
					texture2D.SetPixel(i, j, Color.clear);
				}
			}
			for (int k = 0; k < texture.width; k++)
			{
				for (int l = 0; l < texture.height; l++)
				{
					texture2D.SetPixel(k + TextureStitcher.padding, l + TextureStitcher.padding, texture.GetPixel(k, l));
				}
			}
			texture2D.Apply(false, false);
			return texture2D;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00007040 File Offset: 0x00005240
		public static Texture2D GetReadable(this Texture2D texture)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
			Graphics.Blit(texture, temporary);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = temporary;
			Texture2D texture2D = new Texture2D(texture.width, texture.height);
			texture2D.ReadPixels(new Rect(0f, 0f, (float)temporary.width, (float)temporary.height), 0, 0);
			texture2D.Apply();
			RenderTexture.active = active;
			return texture2D;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000070C4 File Offset: 0x000052C4
		public static Texture2D Rotated(this Texture2D texture, bool clockwise = false)
		{
			Color32[] pixels = texture.GetPixels32();
			Color32[] array = new Color32[pixels.Length];
			int width = texture.width;
			int height = texture.height;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num = (j + 1) * height - i - 1;
					int num2 = clockwise ? (pixels.Length - 1 - (i * width + j)) : (i * width + j);
					array[num] = pixels[num2];
				}
			}
			Texture2D texture2D = new Texture2D(height, width);
			texture2D.SetPixels32(array);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000717C File Offset: 0x0000537C
		public static Texture2D Flipped(this Texture2D texture, bool horizontal = true)
		{
			int width = texture.width;
			int height = texture.height;
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					texture2D.SetPixel(i, j, texture.GetPixel(width - i - 1, j));
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x04000043 RID: 67
		public static readonly int padding = 1;
	}
}
