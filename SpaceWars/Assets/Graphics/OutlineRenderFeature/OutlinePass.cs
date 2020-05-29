using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experiemntal.Rendering.Universal {

  internal class OutlinePass : ScriptableRenderPass {
    public enum RenderTarget {
      Color,
      RenderTexture,
    }

    public Material blitMaterial = null;
    public int blitShaderPassIndex = 0;
    public FilterMode filterMode { get; set; }

    private RenderTargetIdentifier source { get; set; }
    private RenderTargetHandle destination { get; set; }

    RenderTargetHandle temporaryColorTexture;
    string cmdTag;

    public OutlinePass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag) {
      this.renderPassEvent = renderPassEvent;
      this.blitMaterial = blitMaterial;
      this.blitShaderPassIndex = blitShaderPassIndex;
      cmdTag = tag;
      temporaryColorTexture.Init("_TemporaryColorTexture");
    }

    public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination) {
      this.source = source;
      this.destination = destination;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
      CommandBuffer cmd = CommandBufferPool.Get(cmdTag);

      RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
      opaqueDesc.depthBufferBits = 0;

      // Can't read and write to same color target, create a temp render target to blit. 
      if (destination == RenderTargetHandle.CameraTarget) {
        cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDesc, filterMode);
        Blit(cmd, source, temporaryColorTexture.Identifier(), blitMaterial, blitShaderPassIndex);
        Blit(cmd, temporaryColorTexture.Identifier(), source);
      } else {
        Blit(cmd, source, destination.Identifier(), blitMaterial, blitShaderPassIndex);
      }

      context.ExecuteCommandBuffer(cmd);
      CommandBufferPool.Release(cmd);
    }

    /// <inheritdoc/>
    public override void FrameCleanup(CommandBuffer cmd) {
      if (destination == RenderTargetHandle.CameraTarget)
        cmd.ReleaseTemporaryRT(temporaryColorTexture.id);
    }
  }
}