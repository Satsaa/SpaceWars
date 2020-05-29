using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experiemntal.Rendering.Universal {
  public class Outline : ScriptableRendererFeature {
    [System.Serializable]
    public class BlitSettings {
      public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

      public Material blitMaterial = null;
      public int blitMaterialPassIndex = -1;
      public Target destination = Target.Color;
      public string textureId = "_OutlinePassTexture";
    }

    public enum Target {
      Color,
      Texture
    }

    public BlitSettings settings = new BlitSettings();
    RenderTargetHandle renderTextureHandle;

    OutlinePass outlinePass;

    public override void Create() {
      var passIndex = settings.blitMaterial != null ? settings.blitMaterial.passCount - 1 : 1;
      settings.blitMaterialPassIndex = Mathf.Clamp(settings.blitMaterialPassIndex, -1, passIndex);
      outlinePass = new OutlinePass(settings.Event, settings.blitMaterial, settings.blitMaterialPassIndex, name);
      renderTextureHandle.Init(settings.textureId);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
      var src = renderer.cameraDepth;
      var dest = (settings.destination == Target.Color) ? RenderTargetHandle.CameraTarget : renderTextureHandle;

      if (!settings.blitMaterial) return;

      outlinePass.Setup(src, dest);
      renderer.EnqueuePass(outlinePass);
    }
  }
}

