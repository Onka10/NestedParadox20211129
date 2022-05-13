using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.UI;

namespace NestedParadox.UI{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UI_DrawEnergy  _draw;
        [SerializeField] UI_footer  _footer;
        [SerializeField] UI_PlayerHP _hp;
        // Start is called before the first frame update
        void Start()
        {
            _draw.Init();
            _footer.Init();
            _hp.Init();
        }
    }
}
