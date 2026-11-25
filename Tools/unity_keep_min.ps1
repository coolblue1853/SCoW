# SCoW 에디터 창이 앞으로 올라오면 바로 최소화(비활성)한다. 사용: powershell -File Tools/unity_keep_min.ps1 -Seconds 10800
# (11-21 사용자: 에디터 포커스 올라오는 것 막기. 기동·도메인 리로드 때 Unity 가 창을 복원하는 문제 대응)
param([int]$Seconds = 0)
Add-Type @"
using System; using System.Text; using System.Runtime.InteropServices;
public class W {
  public delegate bool P(IntPtr h, IntPtr l);
  [DllImport("user32.dll")] public static extern bool EnumWindows(P f, IntPtr l);
  [DllImport("user32.dll")] public static extern int GetWindowText(IntPtr h, StringBuilder s, int n);
  [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
  [DllImport("user32.dll")] public static extern bool IsIconic(IntPtr h);
  [DllImport("user32.dll")] public static extern bool ShowWindowAsync(IntPtr h, int c);
  [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr h, out uint p);
}
"@
$end = (Get-Date).AddSeconds($Seconds)
do {
  $unity = @(Get-CimInstance Win32_Process -Filter "Name='Unity.exe'" -ErrorAction SilentlyContinue | Where-Object { $_.CommandLine -match 'SCoW' } | ForEach-Object { [int]$_.ProcessId })
  [W]::EnumWindows({ param($h,$l)
    $sb = New-Object Text.StringBuilder 512; [void][W]::GetWindowText($h,$sb,512); $t = $sb.ToString()
    $procId = 0; [void][W]::GetWindowThreadProcessId($h,[ref]$procId)
    if ($unity -contains [int]$procId -and [W]::IsWindowVisible($h) -and -not [W]::IsIconic($h) -and $t -ne '') {
      [void][W]::ShowWindowAsync($h,7); Write-Output "min: $t"
    }
    return $true }, [IntPtr]::Zero) | Out-Null
  if ($Seconds -gt 0) { Start-Sleep -Milliseconds 700 }
} while ((Get-Date) -lt $end)
