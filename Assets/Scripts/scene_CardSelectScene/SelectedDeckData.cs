using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDeckData : MonoBehaviour
{

    // singleton�𗘗p���Ă��܂��B
    // �f�b�L�̃f�[�^�ɃA�N�Z�X����Ƃ���
    // SelectedDeckData.instance.deckData�ɃA�N�Z�X����ƁAint�^�z����擾���邱�Ƃ��ł���͂��ł��B
    public static SelectedDeckData instance;
    public int[] deckData;

    private void Awake()
    {
        instance = this;
    }

}
