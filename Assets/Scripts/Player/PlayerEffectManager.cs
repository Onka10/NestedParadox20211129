using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Players{
    public class PlayerEffectManager : Singleton<PlayerEffectManager>
    {
        public GameObject[] particles = new GameObject[5];
        //攻撃力上昇が0
        //被ダメージが1
        //与ダメージが2
        //移動が3
        //LastSwordが4

        void Start(){

        }

        public void EffectPlay(int p)
        {
            // // pos = this.transform.position;
            // // particle.transform.position = pos;
            // // Vector3 minus = new Vector3(0,-1.7f,0);

            // // Instantiate(particles[p],pos+offset, Quaternion.Euler(x, y, z)).transform.SetParent(this.gameObject.transform);
            // //子オブジェクトにするからoffset いらんかも
            // pos = new Vector3(0,0,0);
            // Instantiate(particles[p],pos, Quaternion.Euler(x, y, z)).transform.SetParent(this.gameObject.transform);
            // p=1;

            // var b = Instantiate(particles[p], new Vector3(1, 0, 0), Quaternion.identity);

            // ParticleSystemを取得
            // var bigBangParticle = particles[p].GetComponent<ParticleSystem>().Play();

            particles[p].SetActive(true);
            particles[p].GetComponent<ParticleSystem>().Play();

            // 表示
            // bigBangParticle.Play();

        }

        public void EffectStop(int p){
            particles[p].SetActive(false);

        }

        public void EffectStopALL(){
            for(int i=0;i<particles.Length;i++){
                particles[i].SetActive(false);
            }
        }
    }
}
