using UnityEngine;

public struct LineDrawer
{
    private LineRenderer _lineRenderer;
    private float _lineSize;

    public LineDrawer(float lineSize = 0.05f)
    {
        GameObject lineGO = new GameObject("Line");
        _lineRenderer = lineGO.AddComponent<LineRenderer>();
        //Particles/Additive
        _lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));

        _lineSize = lineSize;
    }

    private void Initialize(float lineSize = 0.05f)
    {
        GameObject lineObj = new GameObject("Line");
        _lineRenderer = lineObj.AddComponent<LineRenderer>();
        //Particles/Additive
        _lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));

        _lineSize = lineSize;
    }

    //Draws lines through the provided vertices
    public void DrawLine(Vector3 start, Vector3 end, Color color, float lineSize)
    {
        if (_lineRenderer == null)
            Initialize(lineSize);

        //Set color
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;

        //Set width
        _lineRenderer.startWidth = _lineSize;
        _lineRenderer.endWidth = _lineSize;

        //Set line count which is 2
        _lineRenderer.positionCount = 2;

        //Set the postion of both two lines
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
    }

    public void DestroyLine()
    {
        if (_lineRenderer != null)
        {
            Object.Destroy(_lineRenderer.gameObject);
        }
    }
}
