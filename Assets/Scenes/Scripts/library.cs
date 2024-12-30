using Unity.VisualScripting;
using UnityEngine;

/**
 * @file Objectlibrary.cs
 * @brief UnityにおけるC#にて、主にコンポーネントを簡潔に扱えるようになるライブラリの役割を果たすファイル。
 * @note 名前空間GameObjectlibを使用
 */


namespace GameObjectlib
{
    /**
     * @class Container
     * @brief コンストラクタにてオブジェクトを取得、その後Transform等のコンポーネント複数をメンバ変数に格納する。　
        格納するためのメンバ変数が下記の３つであるため、その他コンポーネントを追加したい場合は継承を使うのが無難
     * @par 格納したコンポーネントは簡潔な記述のため略字になっているので注意
     * @brief以下略字対応
     * @detail Obj→GameObject
     * @detail Trs→Transform
     * @detail Rig→Rigidbody
     */
    public class Container
    {
        public GameObject Obj;
        public Rigidbody Rig;
        //@param trs Transform型は座標などで頻繁に変更を加えることを想定しprivateとする。Trsプロパティにて値を取得、変更する。
        private Transform trs;

        private Component component;
        /**
         * @param Vector3 vector3
         * @brief Transformでの位置変更などに使う、一回のインスタンス生成で済ますために使用
         */
        private Vector3 vector3;
        public Rigidbody rigidbody
        {
            get
            {
                return Rig;
            }
            set
            {
                Rig = value;
            }
        }
        public Transform transform
        {
            get
            {
                return trs;
            }
            set
            {
                trs = value;
            }
        }
        public Component Com
        {
            get
            {
                return this.component;
            }
        }
        public Transform Trs
        {
            get
            {
                return trs;
            }
            set
            {
                trs = value;
            }
        }
        /**
         * @var Vector3 At
         * @brief このオブジェクトの座標を得るためのプロパティ
         * @date 2024-8-22 これが無い場合Obj.Trs.positionというように長くなってしまうため\n
         * Obj.Atのように使うため追加した
         */
        public Vector3 At
        {
            get
            {
                return trs.position;
            }
        }
        /**
         * @fn public Container(GameObject Obj)
         * @brief 引数から受け取ったオブジェクトからTransform、Rigidbodyを取得し、格納するコンストラクタ
         * @warning ObjにRigidbodyが無い場合もあり得るため、その際にRigを呼び出してもNULLが返ってくる。
         */
        public Container(GameObject Obj)
        {
            this.Obj = Obj;
            this.Trs = Obj.GetComponent<Transform>();
            this.Rig = Obj.GetComponent<Rigidbody>();
            this.vector3 = new Vector3(0f, 0f, 0f);
        }

        public void SetComponent<T>() where T : Component, new()
        {
            this.component = this.Obj.GetComponent<T>();
        }

        /**
         * @fn void UpdateC()
         * @brief Rigidbodyの情報を更新する際に使う
         */
        public void UpdateC()
        {
            this.Rig = this.Obj.GetComponent<Rigidbody>();
        }


        /**
         * @fn public static implicit operator GameObject(Container a)
         * @brief Container型変数(今回はaとする)のメンバ変数Objを省略時、デフォルトでGameObjectとして返す関数。
         * @par 例→Debug.Log(a);//Debug.Log(a.Obj)と同値
         * @note GameObject型はどのUnity C#スクリプトで一般に高い使用頻度であるためこの型にした。
         * @note なので作るものによりTrsのほうがいいかもしれないのでその場合このクラス継承、オーバーロードで変更。
         * @detail 変更例→public static implicit operator Transform(Container a){return a.Trs;}
         * @warning 暗黙的変換を行っているため、型の扱いが不安な場面(インスタンス生成の引数として渡す等)での使用は避けるのが懸命。
         */
        public static implicit operator GameObject(Container a)
        {
            return a.Obj;
        }

        /**
         *  @fn public static Container operator+(Container a,float x)
         *  @brief Container型変数から直接x座標を変更するための演算子オーバーロード関数
         *  @note positionはVector3型なのでxを元にそのインスタンスを一時生成
         *  @warning 引数が一つであった場合問答無用でxを変更するためa = a + y;と記述してしまった場合position.yではなくposition.xの値が変わるため注意
         *  @param Container a:演算対象オブジェクト
         *  @param float x:a.Transform.position.xに加算するfloat型の変数(定数も可)
         *  @return position変更後のContainer型
         */
        public static Container operator +(Container a, float x)
        {
            a.vector3.x = x;
            a.Trs.position += a.vector3;
            return a;
        }
        /**
         *  @fn public static Container operator+(Container a,(float x, float y) b)
         *  @brief 上記関数のx座標とy座標とを同時に加算するバージョンの関数
         *  @detail 引数にタプルを使用しているためa = a + x + y のように書かず以下の使用例のように書く
         *  @note a = a + (x,y);またはa+=(x,y)
         */
        public static Container operator +(Container a, (float x, float y) b)
        {
            a.vector3.x = b.x;
            a.vector3.y = b.y;
            a.Trs.position += a.vector3;
            return a;
        }
        /**  @fn public static Container operator +(Container a, (float x, float y) b)
        *  @brief 上記関数のx座標とy座標とz座標とを同時に加算するバージョンの関数
        *  @note 使用例 a = a + (x,y,z);またはa+=(x,y)
*/
        public static Container operator +(Container a, (float x, float y, float z) b)
        {
            a.vector3.x = b.x;
            a.vector3.y = b.y;
            a.vector3.z = b.z;
            a.Trs.position += a.vector3;
            return a;
        }


        public static Container operator +(Container a, Vector3 b)
        {
            a.Trs.position += b;
            return a;
        }

        public static Container operator -(Container a, Vector3 b)
        {
            a.Trs.position -= b;
            return a;
        }

        public static Container operator *(Container a, (float x, float y, float z) b)
        {
            a.vector3.x = b.x;
            a.vector3.y = b.y;
            a.vector3.z = b.z;
            a.Rig.AddForce(a.vector3);
            return a;
        }

        public static Container operator *(Container a, Vector3 b)
        {
            a.Rig.AddForce(b);
            return a;
        }
        /**
        * @fn public static Container operator<<(Container a,(floatl x,float y,float z)b)
        * @param Contianer a : 座標の変更対象のオブジェクト
        * @param (float x,float y,float z)b : タプル型で指定する座標
        * @brief 第二引数で指定した座標にする演算子オーバーロード
        * @note　C#では=演算子のオーバーロードが出来ないため、代わりに&演算子を使用した\n
代わりにいいものがあったらそれにする。
*/
        public static Container operator &(Container a, (float x, float y, float z) b)
        {
            a.vector3.x = b.x;
            a.vector3.y = b.y;
            a.vector3.z = b.z;
            a.Trs.position = a.vector3;
            return a;
        }
        public static Container operator &(Container a, Vector3 b)
        {
            a.Trs.position = b;
            return a;
        }
        public static Container operator !(Container a)
        {
            a.Obj.SetActive(!a.Obj.activeSelf);
            return a;
        }
    };


    /**
     * @class CameraContainer
     * @brief カメラオブジェクトの操作において、角度調整の記述を省略する為のクラス
*/
    public class CameraContainer
    {
        /**
         * @var public Container Con
         * @brief コンストラクタより初期化したコンテナクラス
         * @details TransformやRigidbodyなどにアクセスするときはこの変数から
         */
        public Container Con;
        /**
         * @var public Container Cam
         * @brief カメラクラスにアクセスするためのメンバ変数
         * @note カメラクラスのプロパティorthographicへのアクセスは変数StatusおよびDimensionプロパティにて管理しているが、それ以外のメンバにアクセスする際にCamを使う
         */
        public Camera Cam;
        public CameraContainer(byte status, Vector3 center)
        {
            this.Status = status;
            this.Center = center;

        }
        private byte Status { get; set; }//2のとき2D,3のとき3D
        public byte Dimension//このプロパティからでしかStatusにはアクセスできず、読み取りのみ、2と3との切り替えはCdimension()にて
        {
            get
            {
                return Status;
            }
        }

        private Vector3 Axis = Vector3.up;//初期値：Y軸を中心とする
                                          //関数ChangeAxisX,Y,Zにて変更する
        private Vector3 center;//　<<演算子のオーバーロードにて使用したRotateAroundの引数に使う、この座標を中心にして回転させる。
        public Vector3 Center { get; }
        /**
         * @fn public CameraContainer(GameObject obj)
         * @param obj : Cameraコンポーネントを持つGameObject型
         * @brief CameraContainerクラスのコンストラクタ
         * @details カメラオブジェクトを元に、Cameraコンポーネント型の変数Camに格納、
         * @details centerの初期値をカメラオブジェクトの座標にする。
         * @details othographicの真偽でStatusに２もしくは３を割り振る
         * @note othographicが真→二次元的な視点になるためStatusは2になる\nothographicが偽→三次元的視点になるためStatusの値は３になる。
         */
        public CameraContainer(GameObject obj)
        {
            Con = new Container(obj);
            Cam = obj.GetComponent<Camera>();
            Center = Con.At;
            if (this.Cam == null)
            {
                Debug.Log("Cameraコンポーネントがありませんでした");
            }

            if (this.Cam.orthographic == false)
            {
                Status = 3;
            }
            else
            {
                Status = 2;
            }
        }

        /**
         * @fn public void CenterPosition(float x,float y,float z)
         * @param float x : 中心となるオブジェクトのx座標
         * @param float y : 中心となるオブジェクトのy座標
         * @param float z : 中心となるオブジェクトのz座標 
         * @brief　回転するにあたって中心座標を設定するメソッド
         * @details　受け取った引数の座標をVector3型に変換しメンバ変数centerに格納する
         * @note これを設定してカメラを<<演算子によって回転させると、この座標を中心にカメラが向いたまま任意の角度を回転させることができる。\n
         * 公転のイメージ
         */
        public void CenterPosition(float x, float y, float z)
        {
            this.center = new Vector3(x, y, z);
        }
        /**
         * @fn public void CenterPosition(Vector3 center)
         * @param Vector3 center : 中心となるオブジェクトのVector3型の座標
         * @brief　上記関数のVector3バージョン、centerの二通りの設定ができる
         * @note Container型のオブジェクトの座標を引数とするときCenterPosition(Obj.At);という記述になり簡潔になるのでオススメ
         */
        public void CenterPosition(Vector3 center)
        {
            this.center = center;
        }

        /**
         * @fn public void Cdimension()
         * @brief　orthographicの真偽をStatusと対応させたまま変更するメソッド
         * @details 先にStatusの値を変えその値を元にorthographicを変更する\n
         * これによりStatusとorthographicの対応に齟齬が起こってもこのメソッドを呼び出すことで直る
         * @note Statusの値の変更は１と最下位ビットのXOR演算によるもの\n
         * Status=2のとき：10(二進数)と1(二進数)一桁目のXORは0と1になる。そして0≠1であるから一桁目が１になり11(3(十進数))になる\n
         * Status=3のとき：11(二進数)と1(二進数)一桁目のXORは1と1になる。そして1=1であるから一桁目が１になり10(2(十進数))になる\n
         * このことからStatusの値は2と3以外の値に変化しない。
         */
        public void Cdimension()
        {
            this.Status ^= 1;//2か3で切り替え
            if (this.Status == 2)
            {
                this.Cam.orthographic = true;
            }
            else
            {
                this.Cam.orthographic = false;
            }
        }
        /**
         * @fn public void ChangeAxisY()
         * @brief カメラを回転させるにあたって軸を設定する関数
         * @details 初期値はY軸であるが、その場合回転によって変化する座標がx,z座標のみ変化する\n
         * 軸を記録しているメンバ変数はAxis、
         */
        public void ChangeAxisY()
        {
            this.Axis = Vector3.up;
        }
        /**
         * @fn public void ChangeAxisX()
         * @brief 回転させる中心の軸をX軸に設定する関数
         * @details これを適用するとCosの値→z、Sinの値→y
         */
        public void ChangeAxisX()
        {
            this.Axis = Vector3.right;
        }
        /**
         * @fn public void ChangeAxisZ()
         * @brief 回転させる中心の軸をZ軸に設定する関数
         * @details これを適用するとCosの値→x、Sinの値→y
         */
        public void ChangeAxisZ()
        {
            this.Axis = Vector3.forward;
        }
        /**
        * @fn public static implict operator Container(CameraContainer a)
        * @brief メンバーの省略時にContainerクラスを返すメソッド
        * @warning 明示的な変換ではないため、文脈的に型が明らかな場合にのみ使うように
        */
        public static implicit operator Container(CameraContainer a)
        {
            return a.Con;
        }
        /**
         * @fn public static CameraContainer opertor <<(CameraContainer sight,int degree)
         * @param Container sight : 回転する対象オブジェクト
         * @param int degree : 回転させる角度int型なので注意
         * @brief Camera <<= 1 というようにすると１°回転するための演算子オーバーロード
         * @note 中心軸と中心座標に関する変更はそれぞれChangeAxisX,Y,ZとCenterPosition関数を参照
         */
        public static CameraContainer operator <<(CameraContainer sight, int degree)
        {
            sight.Con.Trs.RotateAround(
                sight.center,
                sight.Axis,
                degree
            );
            return sight;
        }
        /**
         * @fn public static CameraContainer operator >>(CameraContainer sight,int degree)
         * @brief Camera >>= 1 というようにすると-1°回転する、要するに左回り
         */
        public static CameraContainer operator >>(CameraContainer sight, int degree)
        {
            sight.Con.Trs.RotateAround(
                sight.center,
                sight.Axis,
                -degree
            );
            return sight;
        }
    };


    public class ObjectReuse
    {
        private Container container;
        private Container copy;
        public ObjectReuse(GameObject Obj)
        {
            container = new Container(Obj);
            copy = container;
        }
        public ObjectReuse(Container Con)
        {
            container = Con;
        }
        private void Reuse()
        {
            container = copy;
        }

        private void InitialStatus()
        {

        }
    }
}