using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Ant : MonoBehaviour
{

    private float _nextTime = 0f;

    private enum Colors
    {
        YELLOW,
        GREEN,
        BLUE,
        RED
    }

    private Colors[] _colors = new []{Colors.YELLOW, Colors.GREEN, Colors.YELLOW, Colors.GREEN, Colors.RED, Colors.BLUE,Colors.RED, Colors.BLUE,Colors.RED, Colors.BLUE,Colors.RED, Colors.BLUE};
    private int _nextColor = 0;

    private Dictionary<Vector3, Colors> _blocks = new Dictionary<Vector3, Colors>();
    
    private void Update()
    {
        if (_nextTime < Time.time)
        {
            if (_blocks.ContainsKey(transform.position))
            {
                var color = _blocks[transform.position];
                switch (color)
                {
                    case Colors.YELLOW:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 90, transform.eulerAngles.y,
                            transform.eulerAngles.z);
                        color = Colors.GREEN;

                        break;
                    case Colors.GREEN:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x - 90, transform.eulerAngles.y,
                            transform.eulerAngles.z);
                        color = Colors.YELLOW;
                        break;
                    case Colors.BLUE:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90,
                            transform.eulerAngles.z);
                        color = Colors.RED;
                        break;
                    default:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 90,
                            transform.eulerAngles.z);
                        color = Colors.BLUE;
                        break;
                }
                _blocks[transform.position] = color;
                transform.Translate(Vector3.forward);
                _nextTime = Time.time + 1f;
            }
            else
            {
                var color = _colors[_nextColor];
                _blocks.Add(transform.position,color);
                _nextColor += 1;
                if (_nextColor == _colors.Length)
                {
                    _nextColor = 0;
                }
            }
        }
    }


    void Next()
    {
        transform.position = new Vector3(transform.position.x + 1,transform.position.y,transform.position.z);
    }

    private void OnDrawGizmos()
    {
        foreach (var key in _blocks.Keys)
        {
            var color = _blocks[key];
            switch (color)
            {
                case Colors.YELLOW:
                    Gizmos.color = Color.yellow;
                    break;
                case Colors.GREEN:
                    Gizmos.color = Color.green;
                    break;
                case Colors.BLUE:
                    Gizmos.color = Color.blue;
                    break;
                default:
                    Gizmos.color = Color.red;
                    break;
            }
            Gizmos.DrawCube(key, Vector3.one);
        }
    }
}
