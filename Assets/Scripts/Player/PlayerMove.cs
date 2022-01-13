using UniRx;
using UnityEngine;

namespace NestedParadox.Players
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;
        [SerializeField] Transform mytransform;
        [SerializeField] float jumpPower;
        [SerializeField] float gravityPower;

        //行動不能
        private bool _isMoveBlock;

        private void FixedUpdate()
        {
            rb.AddForce(new Vector3(0, -1 * gravityPower));
        }

        public void Move(int direction)
        {
            if(direction == 1)
            {
                mytransform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction == -1)
            {
                mytransform.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void Jump()
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Force);
        }

        // 移動不可フラグ
        public void BlockMove(bool isBlock)
        {
            _isMoveBlock = isBlock;
        }
    }
}
