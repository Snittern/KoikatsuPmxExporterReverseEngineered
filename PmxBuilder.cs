// Decompiled with JetBrains decompiler
// Type: PmxBuilder
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using PmxLib;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

internal class PmxBuilder
{
    private string msg = "";
    private Pmx pmxFile;
    private string pass = "C:\\koikatsu_model\\";
    private int vertexCount = 0;
    private int scale = 12;
    private Dictionary<Transform, int> bonesMap;
    private List<Transform> boneList = new List<Transform>();
    private List<Matrix4x4> bindposeList = new List<Matrix4x4>();
    public List<Bones> bones2 = new List<Bones>();
    private Vers[] vers;
    private int[] vertics_num;
    private string[] vertics_name;
    private List<int>[] hitomi = new List<int>[2];
    private List<SkinnedMeshRenderer> skinnedMeshList;
    private List<MeshFilter> meshList;
    private string[] bone_name_list = new string[234]
    {
    "cf_s_leg01_L",
    "cf_s_thigh01_L",
    "cf_s_waist01",
    "cf_s_leg02_L",
    "cf_j_foot_L",
    "cf_j_toes_L",
    "cf_s_thigh02_L",
    "cf_s_kneeB_L",
    "cf_s_thigh03_L",
    "cf_s_thigh01_R",
    "cf_s_leg02_R",
    "cf_j_foot_R",
    "cf_j_toes_R",
    "cf_s_leg01_R",
    "cf_s_thigh02_R",
    "cf_s_kneeB_R",
    "cf_s_thigh03_R",
    "cf_s_waist02",
    "cf_j_kokan",
    "cf_s_siri_L",
    "cf_s_siri_R",
    "cf_s_leg_L",
    "cf_s_leg_R",
    "cf_s_neck",
    "cf_s_head",
    "cf_s_hand_L",
    "cf_s_wrist_L",
    "cf_d_hand_L",
    "cf_s_forearm01_L",
    "cf_s_forearm02_L",
    "cf_s_arm01_L",
    "cf_s_arm02_L",
    "cf_s_arm03_L",
    "cf_s_elbo_L",
    "cf_s_shoulder02_L",
    "cf_s_forearm01_R",
    "cf_s_wrist_R",
    "cf_d_hand_R",
    "cf_s_forearm02_R",
    "cf_s_arm01_R",
    "cf_s_arm02_R",
    "cf_s_arm03_R",
    "cf_s_elbo_R",
    "cf_s_shoulder02_R",
    "cf_s_bust02_L",
    "cf_s_bust03_L",
    "cf_s_bnip01_L",
    "cf_s_bust02_R",
    "cf_s_bust03_R",
    "cf_s_bnip01_R",
    "cf_s_leg03_L",
    "cf_s_leg03_R",
    "cf_s_elboback_L",
    "cf_s_elboback_R",
    "cf_s_bust01_L",
    "cf_s_bust01_R",
    "cf_s_spine01",
    "cf_s_spine03",
    "cf_s_spine02",
    "cf_s_hand_R",
    "cf_s_ana",
    "cf_j_bnip02_L",
    "cf_j_bnip02_R",
    "cf_j_little01_L",
    "cf_j_little02_L",
    "cf_j_little03_L",
    "cf_j_ring01_L",
    "cf_j_ring02_L",
    "cf_j_ring03_L",
    "cf_j_middle01_L",
    "cf_j_middle02_L",
    "cf_j_middle03_L",
    "cf_j_index01_L",
    "cf_j_index02_L",
    "cf_j_index03_L",
    "cf_j_thumb01_L",
    "cf_j_thumb02_L",
    "cf_j_thumb03_L",
    "cf_j_thumb01_R",
    "cf_j_thumb02_R",
    "cf_j_thumb03_R",
    "cf_j_index01_R",
    "cf_j_index02_R",
    "cf_j_index03_R",
    "cf_j_middle01_R",
    "cf_j_middle02_R",
    "cf_j_middle03_R",
    "cf_j_ring01_R",
    "cf_j_ring02_R",
    "cf_j_ring03_R",
    "cf_j_little01_R",
    "cf_j_little02_R",
    "cf_j_little03_R",
    "cf_s_bnip025_R",
    "cf_s_bnip025_L",
    "cf_d_kneeF_L",
    "cf_d_kneeF_R",
    "cf_j_ana",
    "cf_s_bnip015_L",
    "cf_s_bnip015_R",
    "cf_J_FaceRoot",
    "cf_J_NoseBase",
    "cf_J_Nose_tip",
    "cf_J_NoseBridge_rx",
    "cf_J_FaceUp_ty",
    "cf_J_FaceUp_tz",
    "cf_J_Eye_rz_L",
    "cf_J_Eye01_s_L",
    "cf_J_Eye02_s_L",
    "cf_J_Eye03_s_L",
    "cf_J_Eye04_s_L",
    "cf_J_Eye05_s_L",
    "cf_J_Eye06_s_L",
    "cf_J_Eye07_s_L",
    "cf_J_Eye08_s_L",
    "cf_J_CheekUp2_L",
    "cf_J_EarBase_ry_L",
    "cf_J_EarUp_L",
    "cf_J_EarLow_L",
    "cf_J_Eye_rz_R",
    "cf_J_Eye01_s_R",
    "cf_J_Eye02_s_R",
    "cf_J_Eye03_s_R",
    "cf_J_Eye04_s_R",
    "cf_J_Eye05_s_R",
    "cf_J_Eye06_s_R",
    "cf_J_Eye07_s_R",
    "cf_J_Eye08_s_R",
    "cf_J_CheekUp2_R",
    "cf_J_EarBase_ry_R",
    "cf_J_EarUp_R",
    "cf_J_EarLow_R",
    "cf_J_ChinLow",
    "cf_J_Chin_s",
    "cf_J_CheekLow_s_L",
    "cf_J_CheekLow_s_R",
    "cf_J_ChinTip_Base",
    "cf_J_CheekUp_s_L",
    "cf_J_CheekUp_s_R",
    "cf_J_MouthMove",
    "cf_J_Mouthup",
    "cf_J_MouthCavity",
    "cf_J_MouthLow",
    "cf_J_Mouth_L",
    "cf_J_Mouth_R",
    "cf_J_MayuTip_s_L",
    "cf_J_MayuMid_s_L",
    "cf_J_MayuTip_s_R",
    "cf_J_MayuMid_s_R",
    "cf_J_Nose_rx",
    "cf_J_Mayumoto_L",
    "cf_J_Mayu_L",
    "cf_J_Mayumoto_R",
    "cf_J_Mayu_R",
    "cf_J_hitomi_tx_L",
    "cf_J_hitomi_tx_R",
    "cf_J_hairF_top",
    "cf_J_hairF_00",
    "cf_J_hairF_01",
    "cf_J_hairF_02",
    "cf_J_hairFR_02_00",
    "cf_J_hairFR_02_01",
    "cf_J_hairFR_02_02",
    "cf_J_hairFL_02_00",
    "cf_J_hairFL_02_01",
    "cf_J_hairFL_02_02",
    "cf_j_sk_00_00",
    "cf_j_sk_00_01",
    "cf_j_sk_00_02",
    "cf_j_sk_00_03",
    "cf_j_sk_00_04",
    "cf_j_sk_00_05",
    "cf_j_sk_01_00",
    "cf_j_sk_01_01",
    "cf_j_sk_01_02",
    "cf_j_sk_01_03",
    "cf_j_sk_01_04",
    "cf_j_sk_01_05",
    "cf_j_sk_02_00",
    "cf_j_sk_02_01",
    "cf_j_sk_02_02",
    "cf_j_sk_02_03",
    "cf_j_sk_02_04",
    "cf_j_sk_02_05",
    "cf_j_sk_06_00",
    "cf_j_sk_06_01",
    "cf_j_sk_06_02",
    "cf_j_sk_06_03",
    "cf_j_sk_06_04",
    "cf_j_sk_06_05",
    "cf_j_sk_07_00",
    "cf_j_sk_07_01",
    "cf_j_sk_07_02",
    "cf_j_sk_07_03",
    "cf_j_sk_07_04",
    "cf_j_sk_07_05",
    "cf_j_sk_03_00",
    "cf_j_sk_03_01",
    "cf_j_sk_03_02",
    "cf_j_sk_03_03",
    "cf_j_sk_03_04",
    "cf_j_sk_03_05",
    "cf_j_sk_04_00",
    "cf_j_sk_04_01",
    "cf_j_sk_04_02",
    "cf_j_sk_04_03",
    "cf_j_sk_04_04",
    "cf_j_sk_04_05",
    "cf_j_sk_05_00",
    "cf_j_sk_05_01",
    "cf_j_sk_05_02",
    "cf_j_sk_05_03",
    "cf_j_sk_05_04",
    "cf_j_sk_05_05",
    "cf_j_spinesk_00",
    "cf_j_spinesk_01",
    "cf_j_spinesk_02",
    "cf_j_spinesk_03",
    "cf_j_spinesk_04",
    "cf_j_spinesk_05",
    "cf_J_hairB_top",
    "j_01",
    "j1",
    "N_move2",
    "N_j_Bwing_R_01",
    "cf_J_hairFL_00",
    "joint1",
    "cf_J_hairS_top",
    "cf_J_hairFL_01_00",
    "cf_J_hairFR_01_00",
    "cf_J_hairB_00",
    "cf_J_hairBR_00",
    "cf_J_hairBL_00",
    "cf_J_hairFR_00"
    };
    private string[] bone_parent_name_list = new string[235]
    {
    "cf_s_thigh01_L",
    "cf_s_waist02",
    "",
    "cf_s_leg01_L",
    "cf_s_leg01_L",
    "cf_j_foot_L",
    "cf_s_thigh01_L",
    "cf_s_leg01_L",
    "cf_s_thigh02_L",
    "cf_s_waist02",
    "cf_s_leg01_R",
    "cf_s_leg01_R",
    "cf_j_foot_R",
    "cf_s_thigh01_R",
    "cf_s_thigh01_R",
    "cf_s_leg01_R",
    "cf_s_thigh02_R",
    "",
    "cf_s_waist02",
    "cf_s_waist02",
    "cf_s_waist02",
    "cf_s_waist02",
    "cf_s_waist02",
    "cf_s_spine03",
    "cf_s_neck",
    "cf_s_forearm01_L",
    "cf_s_forearm01_L",
    "cf_s_forearm01_L",
    "cf_s_arm01_L",
    "cf_s_forearm01_L",
    "cf_s_shoulder02_L",
    "cf_s_arm01_L",
    "cf_s_arm02_L",
    "cf_s_forearm01_L",
    "cf_s_spine03",
    "cf_s_arm01_R",
    "cf_s_forearm02_R",
    "cf_s_wrist_R",
    "cf_s_forearm01_R",
    "cf_s_shoulder02_R",
    "cf_s_arm01_R",
    "cf_s_arm02_R",
    "cf_s_forearm01_R",
    "cf_s_spine03",
    "cf_s_bust01_L",
    "cf_s_bust02_L",
    "cf_s_bust03_L",
    "cf_s_bust01_R",
    "cf_s_bust02_R",
    "cf_s_bust03_R",
    "cf_s_leg02_L",
    "cf_s_leg02_R",
    "cf_s_elbo_L",
    "cf_s_elbo_R",
    "cf_s_spine03",
    "cf_s_spine03",
    "cf_s_waist01",
    "cf_s_spine02",
    "cf_s_spine01",
    "cf_d_hand_R",
    "cf_j_ana",
    "cf_s_bnip025_L",
    "cf_s_bnip025_R",
    "cf_s_hand_L",
    "cf_j_little01_L",
    "cf_j_little02_L",
    "cf_s_hand_L",
    "cf_j_ring01_L",
    "cf_j_ring02_L",
    "cf_s_hand_L",
    "cf_j_middle01_L",
    "cf_j_middle02_L",
    "cf_s_hand_L",
    "cf_j_index01_L",
    "cf_j_index02_L",
    "cf_s_hand_L",
    "cf_j_thumb01_L",
    "cf_j_thumb02_L",
    "cf_s_hand_R",
    "cf_j_thumb01_R",
    "cf_j_thumb02_R",
    "cf_s_hand_R",
    "cf_j_index01_R",
    "cf_j_index02_R",
    "cf_s_hand_R",
    "cf_j_middle01_R",
    "cf_j_middle02_R",
    "cf_s_hand_R",
    "cf_j_ring01_R",
    "cf_j_ring02_R",
    "cf_s_hand_R",
    "cf_j_little01_R",
    "cf_j_little02_R",
    "cf_s_bnip015_R",
    "cf_s_bnip015_L",
    "cf_s_leg01_L",
    "cf_s_leg01_R",
    "cf_s_waist02",
    "cf_s_bnip01_L",
    "cf_s_bnip01_R",
    "cf_s_head",
    "cf_J_FaceRoot",
    "cf_J_Nose_rx",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_L",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_EarBase_ry_L",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_Eye_rz_R",
    "cf_J_Eye_rz_R",
    "cf_J_Eye_rz_R",
    "cf_J_Eye_rz_R",
    "cf_J_Eye_rz_R",
    "cf_J_Eye_rz_R",
    "cf_J_Eye_rz_R",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_EarBase_ry_R",
    "cf_J_Chin_s",
    "cf_J_ChinTip_Base",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_Eye_rz_L",
    "cf_J_Eye_rz_R",
    "cf_J_FaceRoot",
    "cf_J_FaceRoot",
    "cf_J_hairF_top",
    "cf_J_hairF_00",
    "cf_J_hairF_01",
    "cf_J_hairF_top",
    "cf_J_hairFR_02_00",
    "cf_J_hairFR_02_01",
    "cf_J_hairF_top",
    "cf_J_hairFL_02_00",
    "cf_J_hairFL_02_01",
    "cf_s_waist01",
    "cf_j_sk_00_00",
    "cf_j_sk_00_01",
    "cf_j_sk_00_02",
    "cf_j_sk_00_03",
    "cf_j_sk_00_04",
    "cf_s_waist01",
    "cf_j_sk_01_00",
    "cf_j_sk_01_01",
    "cf_j_sk_01_02",
    "cf_j_sk_01_03",
    "cf_j_sk_01_04",
    "cf_s_waist01",
    "cf_j_sk_02_00",
    "cf_j_sk_02_01",
    "cf_j_sk_02_02",
    "cf_j_sk_02_03",
    "cf_j_sk_02_04",
    "cf_s_waist01",
    "cf_j_sk_06_00",
    "cf_j_sk_06_01",
    "cf_j_sk_06_02",
    "cf_j_sk_06_03",
    "cf_j_sk_06_04",
    "cf_s_waist01",
    "cf_j_sk_07_00",
    "cf_j_sk_07_01",
    "cf_j_sk_07_02",
    "cf_j_sk_07_03",
    "cf_j_sk_07_04",
    "cf_s_waist01",
    "cf_j_sk_03_00",
    "cf_j_sk_03_01",
    "cf_j_sk_03_02",
    "cf_j_sk_03_03",
    "cf_j_sk_03_04",
    "cf_s_waist01",
    "cf_j_sk_04_00",
    "cf_j_sk_04_01",
    "cf_j_sk_04_02",
    "cf_j_sk_04_03",
    "cf_j_sk_04_04",
    "cf_s_waist01",
    "cf_j_sk_05_00",
    "cf_j_sk_05_01",
    "cf_j_sk_05_02",
    "cf_j_sk_05_03",
    "cf_j_sk_05_04",
    "cf_s_neck",
    "cf_j_spinesk_00",
    "cf_j_spinesk_01",
    "cf_j_spinesk_02",
    "cf_j_spinesk_03",
    "cf_j_spinesk_04",
    "cf_J_FaceRoot",
    "cf_J_hairF_top",
    "cf_s_spine03",
    "j1",
    "cf_s_spine03",
    "cf_J_hairF_top",
    "cf_J_hairB_top",
    "cf_J_hairF_top",
    "cf_J_hairF_top",
    "cf_J_hairF_top",
    "cf_J_hairB_top",
    "cf_J_hairB_top",
    "cf_J_hairB_top",
    "cf_J_hairF_top",
    "cf_J_hairF_top"
    };
    private string[] bone_change_name_base = new string[55]
    {
    "cf_j_spine01",
    "cf_j_spine02",
    "cf_j_neck",
    "cf_j_head",
    "cf_j_waist01",
    "cf_J_hitomi_tx_L",
    "cf_J_hitomi_tx_R",
    "cf_d_shoulder02_L",
    "cf_j_arm00_L",
    "cf_j_arm03_L",
    "cf_j_forearm01_L",
    "cf_j_forearm02_L",
    "cf_j_hand_L",
    "cf_j_ring01_L",
    "cf_j_ring02_L",
    "cf_j_ring03_L",
    "cf_j_thumb01_L",
    "cf_j_thumb02_L",
    "cf_j_thumb03_L",
    "cf_j_middle01_L",
    "cf_j_middle02_L",
    "cf_j_middle03_L",
    "cf_j_little01_L",
    "cf_j_little02_L",
    "cf_j_little03_L",
    "cf_j_index01_L",
    "cf_j_index02_L",
    "cf_j_index03_L",
    "cf_j_thigh00_L",
    "cf_j_leg01_L",
    "cf_j_foot_L",
    "cf_d_shoulder02_R",
    "cf_j_arm00_R",
    "cf_j_arm03_R",
    "cf_j_forearm01_R",
    "cf_j_forearm02_R",
    "cf_j_hand_R",
    "cf_j_ring01_R",
    "cf_j_ring02_R",
    "cf_j_ring03_R",
    "cf_j_thumb01_R",
    "cf_j_thumb02_R",
    "cf_j_thumb03_R",
    "cf_j_middle01_R",
    "cf_j_middle02_R",
    "cf_j_middle03_R",
    "cf_j_little01_R",
    "cf_j_little02_R",
    "cf_j_little03_R",
    "cf_j_index01_R",
    "cf_j_index02_R",
    "cf_j_index03_R",
    "cf_j_thigh00_R",
    "cf_j_leg01_R",
    "cf_j_foot_R"
    };
    private string[] bone_change_name = new string[55]
    {
    "上半身",
    "上半身2",
    "首",
    "頭",
    "下半身",
    "左目x",
    "右目x",
    "左肩",
    "左腕",
    "左腕捩",
    "左ひじ",
    "左手捩",
    "左手首",
    "左薬指１",
    "左薬指２",
    "左薬指３",
    "左親指０",
    "左親指１",
    "左親指２",
    "左中指１",
    "左中指２",
    "左中指３",
    "左小指１",
    "左小指２",
    "左小指３",
    "左人指１",
    "左人指２",
    "左人指３",
    "左足",
    "左ひざ",
    "左足首",
    "右肩",
    "右腕",
    "右腕捩",
    "右ひじ",
    "右手捩",
    "右手首",
    "右薬指１",
    "右薬指２",
    "右薬指３",
    "右親指０",
    "右親指１",
    "右親指２",
    "右中指１",
    "右中指２",
    "右中指３",
    "右小指１",
    "右小指２",
    "右小指３",
    "右人指１",
    "右人指２",
    "右人指３",
    "右足",
    "右ひざ",
    "右足首"
    };

    public PmxBuilder() => this.pmxFile = new Pmx();

    public string BuildStart()
    {
        try
        {
            this.pass = this.pass + (object)new System.Random().Next(9999) + "\\";
            Directory.CreateDirectory(this.pass);
            this.hitomi[0] = new List<int>();
            this.hitomi[1] = new List<int>();
            this.setSkinnedMeshList();
            this.CreateModelInfo();
            this.CreateBoneList();
            this.CreateMeshList();
            this.addAccessory();
            this.createmorph();
            this.setmaterial();
            this.changebonename();
            this.addbone();
            this.changeboneinfo();
            this.addmorph();
            this.addphysics();
            this.addhitomimorph();
            this.addnode();
            this.phymune();
            this.CreatePmxHeader();
            this.Save();
            this.msg += "\n";
        }
        catch (Exception ex)
        {
            this.msg = this.msg + (object)ex + "\n";
        }
        return this.msg;
    }

    private void test_bones()
    {
        Transform transform = GameObject.Find("BodyTop").transform;
        List<Transform> transformList = new List<Transform>();
        Transform[] componentsInChildren = ((Component)transform).GetComponentsInChildren<Transform>();
        int count = this.pmxFile.BoneList.Count;
        PmxBone pmxBone1 = new PmxBone();
        pmxBone1.Name = (transform).name;
        pmxBone1.Parent = transformList.IndexOf(transform.parent) + count;
        Vector3 vector3_1 = Vector3.op_Multiply(((Component)transform).transform.position, (float)this.scale);
        pmxBone1.Position = new Vector3((float)-vector3_1.x, (float)vector3_1.y, (float)-vector3_1.z);
        transformList.Add(transform);
        this.pmxFile.BoneList.Add(pmxBone1);
        for (int index = 0; index < componentsInChildren.Length; ++index)
        {
            PmxBone pmxBone2 = new PmxBone();
            pmxBone2.Name = (componentsInChildren[index]).name;
            pmxBone2.Parent = transformList.IndexOf(componentsInChildren[index].parent) + count;
            Vector3 vector3_2 = Vector3.op_Multiply(((Component)componentsInChildren[index]).transform.position, (float)this.scale);
            pmxBone2.Position = new Vector3((float)-vector3_2.x, (float)vector3_2.y, (float)-vector3_2.z);
            transformList.Add(componentsInChildren[index]);
            this.pmxFile.BoneList.Add(pmxBone2);
        }
    }

    private void addhitomimorph()
    {
        int num1 = 0;
        foreach (Object componentsInChild in ((Component)GameObject.Find("BodyTop").transform).GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            ++num1;
            if (componentsInChild.name.Contains("hitomi"))
                break;
        }
        int num2 = 0;
        for (int index = 0; index < num1 + 1; ++index)
            num2 += this.vertics_num[index];
        if (num2 >= this.pmxFile.VertexList.Count)
            return;
        this.hitomi[0] = new List<int>();
        int num3;
        for (num3 = 0; num3 < this.vertics_num[num1 + 1]; ++num3)
            this.hitomi[0].Add(num2 + num3);
        this.hitomi[1] = new List<int>();
        for (int index = 0; index < this.vertics_num[num1 + 2]; ++index)
            this.hitomi[1].Add(num2 + num3 + index);
        List<Vector2> vector2List = new List<Vector2>();
        List<int> intList = new List<int>();
        float num4 = 0.0f;
        float num5 = 0.0f;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            num4 += this.pmxFile.VertexList[this.hitomi[0][index]].UV.x;
            num5 += this.pmxFile.VertexList[this.hitomi[0][index]].UV.y;
        }
        float num6 = num4 / (float)this.hitomi[0].Count;
        float num7 = num5 / (float)this.hitomi[0].Count;
        float num8 = 0.0f;
        float num9 = 0.0f;
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            num8 += this.pmxFile.VertexList[this.hitomi[1][index]].UV.x;
            num9 += this.pmxFile.VertexList[this.hitomi[1][index]].UV.y;
        }
        float num10 = num8 / (float)this.hitomi[0].Count;
        float num11 = num9 / (float)this.hitomi[0].Count;
        PmxMorph pmxMorph1 = new PmxMorph();
        pmxMorph1.Name = "hitomiX-small";
        pmxMorph1.NameE = "";
        pmxMorph1.Panel = 1;
        pmxMorph1.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(this.pmxFile.VertexList[this.hitomi[0][index]].UV.x - num6, 0.0f));
            pmxMorph1.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(this.pmxFile.VertexList[this.hitomi[1][index]].UV.x - num10, 0.0f));
            pmxMorph1.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph1);
        PmxMorph pmxMorph2 = new PmxMorph();
        pmxMorph2.Name = "hitomiY-small";
        pmxMorph2.NameE = "";
        pmxMorph2.Panel = 1;
        pmxMorph2.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(0.0f, this.pmxFile.VertexList[this.hitomi[0][index]].UV.y - num7));
            pmxMorph2.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(0.0f, this.pmxFile.VertexList[this.hitomi[1][index]].UV.y - num11));
            pmxMorph2.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph2);
        PmxMorph pmxMorph3 = new PmxMorph();
        pmxMorph3.Name = "hitomiX-big";
        pmxMorph3.NameE = "";
        pmxMorph3.Panel = 1;
        pmxMorph3.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2((float)-((double)this.pmxFile.VertexList[this.hitomi[0][index]].UV.x - (double)num6), 0.0f));
            pmxMorph3.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2((float)-((double)this.pmxFile.VertexList[this.hitomi[1][index]].UV.x - (double)num10), 0.0f));
            pmxMorph3.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph3);
        PmxMorph pmxMorph4 = new PmxMorph();
        pmxMorph4.Name = "hitomiY-big";
        pmxMorph4.NameE = "";
        pmxMorph4.Panel = 1;
        pmxMorph4.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(0.0f, (float)-((double)this.pmxFile.VertexList[this.hitomi[0][index]].UV.y - (double)num7)));
            pmxMorph4.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(0.0f, (float)-((double)this.pmxFile.VertexList[this.hitomi[1][index]].UV.y - (double)num11)));
            pmxMorph4.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph4);
        PmxMorph pmxMorph5 = new PmxMorph();
        pmxMorph5.Name = "hitomi-up";
        pmxMorph5.NameE = "";
        pmxMorph5.Panel = 1;
        pmxMorph5.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(0.0f, 0.5f));
            pmxMorph5.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(0.0f, 0.5f));
            pmxMorph5.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph5);
        PmxMorph pmxMorph6 = new PmxMorph();
        pmxMorph6.Name = "hitomi-down";
        pmxMorph6.NameE = "";
        pmxMorph6.Panel = 1;
        pmxMorph6.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(0.0f, -0.5f));
            pmxMorph6.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(0.0f, -0.5f));
            pmxMorph6.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph6);
        PmxMorph pmxMorph7 = new PmxMorph();
        pmxMorph7.Name = "hitomi-left";
        pmxMorph7.NameE = "";
        pmxMorph7.Panel = 1;
        pmxMorph7.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(-0.5f, 0.0f));
            pmxMorph7.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(0.5f, 0.0f));
            pmxMorph7.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph7);
        PmxMorph pmxMorph8 = new PmxMorph();
        pmxMorph8.Name = "hitomi-right";
        pmxMorph8.NameE = "";
        pmxMorph8.Panel = 1;
        pmxMorph8.Kind = PmxMorph.OffsetKind.UV;
        for (int index = 0; index < this.hitomi[0].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[0][index], (Vector4)new Vector2(0.5f, 0.0f));
            pmxMorph8.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        for (int index = 0; index < this.hitomi[1].Count; ++index)
        {
            PmxUVMorph pmxUvMorph = new PmxUVMorph(this.hitomi[1][index], (Vector4)new Vector2(-0.5f, 0.0f));
            pmxMorph8.OffsetList.Add((PmxBaseMorph)pmxUvMorph);
        }
        this.pmxFile.MorphList.Add(pmxMorph8);
    }

    private void addmorph()
    {
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "あ",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_ha.ha00_a_l_op"),
          Ratio = 1f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_face.f00_a_l_op"),
          Ratio = 1f
        }
      }
        });
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "い",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_ha.ha00_i_l_cl"),
          Ratio = 1f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_face.f00_i_l_op"),
          Ratio = 1f
        }
      }
        });
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "う",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_ha.ha00_a_l_op"),
          Ratio = 1f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_face.f00_u_l_op"),
          Ratio = 1f
        }
      }
        });
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "え",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_ha.ha00_e_s_op"),
          Ratio = 1f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_face.f00_e_l_op"),
          Ratio = 1f
        }
      }
        });
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "お",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_ha.ha00_a_l_op"),
          Ratio = 1f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("kuti_face.f00_o_l_op"),
          Ratio = 1f
        }
      }
        });
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "まばたき",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("eye_line_u.elu00_def_cl"),
          Ratio = 0.36f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("eye_line_l.ell00_def_cl"),
          Ratio = 0.36f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("eye_face.f00_def_cl"),
          Ratio = 0.72f
        }
      }
        });
        this.pmxFile.MorphList.Add(new PmxMorph()
        {
            Name = "笑い",
            NameE = "",
            Panel = 1,
            Kind = PmxMorph.OffsetKind.Group,
            OffsetList = {
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("eye_line_u.elu00_egao_cl"),
          Ratio = 0.36f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("eye_line_l.ell00_egao_cl"),
          Ratio = 0.36f
        },
        (PmxBaseMorph) new PmxGroupMorph()
        {
          Index = this.smi("eye_face.f00_egao_cl"),
          Ratio = 0.72f
        }
      }
        });
        PmxMorph pmxMorph = new PmxMorph();
        pmxMorph.Name = "bounse";
        pmxMorph.NameE = "";
        pmxMorph.Panel = 1;
        pmxMorph.Kind = PmxMorph.OffsetKind.Bone;
        PmxBoneMorph pmxBoneMorph1 = new PmxBoneMorph();
        pmxBoneMorph1.Index = this.sbi("右腕");
        Quaternion quaternion1 = Quaternion.Euler(0.0f, 0.0f, 35f);
        pmxBoneMorph1.Rotaion = new Quaternion(new Vector3((float)quaternion1.x, (float)quaternion1.y, (float)quaternion1.z), (float)quaternion1.w);
        pmxMorph.OffsetList.Add((PmxBaseMorph)pmxBoneMorph1);
        PmxBoneMorph pmxBoneMorph2 = new PmxBoneMorph();
        pmxBoneMorph2.Index = this.sbi("左腕");
        Quaternion quaternion2 = Quaternion.Euler(0.0f, 0.0f, -35f);
        pmxBoneMorph2.Rotaion = new Quaternion(new Vector3((float)quaternion2.x, (float)quaternion2.y, (float)quaternion2.z), (float)quaternion2.w);
        pmxMorph.OffsetList.Add((PmxBaseMorph)pmxBoneMorph2);
        this.pmxFile.MorphList.Add(pmxMorph);
    }

    private void changebonename()
    {
        for (int index = 0; index < this.bone_change_name_base.Length; ++index)
        {
            try
            {
                this.serchBone(this.bone_change_name_base[index]).Name = this.bone_change_name[index];
            }
            catch (Exception ex)
            {
            }
        }
        this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust01_L")), new PmxBone()
        {
            Name = "胸親"
        });
        this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust02_L")) + 1, new PmxBone()
        {
            Name = "左AH2"
        });
        this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust02_R")) + 1, new PmxBone()
        {
            Name = "右AH2"
        });
        this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust01_L")), new PmxBone()
        {
            Name = "左胸操作"
        });
        this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("cf_j_bust01_R")), new PmxBone()
        {
            Name = "右胸操作"
        });
        PmxBone pmxBone1 = this.sb("cf_j_bust02_L");
        pmxBone1.Name = "左AH1";
        pmxBone1.To_Bone = this.sbi("左AH2");
        pmxBone1.Position = new Vector3(this.sb("cf_j_bust01_L").Position);
        pmxBone1.Flags = PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
        PmxBone pmxBone2 = this.sb("左胸操作");
        pmxBone2.Name = "左胸操作";
        pmxBone2.Parent = this.sbi("胸親");
        pmxBone2.Position = this.sb("左AH1").Position + new Vector3(0.0f, 0.0f, -0.213f);
        pmxBone2.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
        PmxBone pmxBone3 = this.sb("左AH2");
        pmxBone3.Parent = this.sbi("左AH1");
        pmxBone3.Position = this.sb("左AH1").Position + new Vector3(0.0f, 0.0f, -1.515f);
        pmxBone3.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Enable;
        PmxBone pmxBone4 = this.sb("cf_j_bust02_R");
        pmxBone4.Name = "右AH1";
        pmxBone4.To_Bone = this.sbi("右AH2");
        pmxBone4.Position = new Vector3(this.sb("cf_j_bust01_R").Position);
        pmxBone4.Flags = PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
        PmxBone pmxBone5 = this.sb("右胸操作");
        pmxBone5.Name = "右胸操作";
        pmxBone5.Parent = this.sbi("胸親");
        pmxBone5.Position = this.sb("右AH1").Position + new Vector3(0.0f, 0.0f, -0.213f);
        pmxBone5.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
        PmxBone pmxBone6 = this.sb("右AH2");
        pmxBone6.Parent = this.sbi("右AH1");
        pmxBone6.Position = this.sb("右AH1").Position + new Vector3(0.0f, 0.0f, -1.515f);
        pmxBone6.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Enable;
        PmxBone pmxBone7 = this.sb("胸親");
        pmxBone7.Parent = this.sbi("上半身2");
        pmxBone7.Position = new Vector3(this.sb("右胸操作").Position);
        pmxBone7.Position.x = 0.0f;
        pmxBone7.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
    }

    private void createmorph()
    {
        ChaControl instance = Singleton<ChaControl>.Instance;
        foreach (FBSTargetInfo fbsTargetInfo in (FBSTargetInfo[])((FBSBase)((ChaInfo)instance).eyesCtrl).FBSTarget)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = fbsTargetInfo.GetSkinnedMeshRenderer();
            int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
            Vector3[] vector3Array1 = new Vector3[skinnedMeshRenderer.sharedMesh.vertices.Length];
            Vector3[] vector3Array2 = new Vector3[skinnedMeshRenderer.sharedMesh.normals.Length];
            Vector3[] vector3Array3 = new Vector3[skinnedMeshRenderer.sharedMesh.tangents.Length];
            int num = 0;
            for (int index = 0; index < this.vertics_num.Length && this.vertics_num[index] != vector3Array1.Length; ++index)
                num += this.vertics_num[index];
            if (num < this.pmxFile.VertexList.Count)
            {
                for (int index1 = 0; index1 < blendShapeCount; ++index1)
                {
                    skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameVertices(index1, 0, vector3Array1, vector3Array2, vector3Array3);
                    PmxMorph pmxMorph = new PmxMorph();
                    pmxMorph.Name = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(index1);
                    pmxMorph.NameE = "";
                    pmxMorph.Panel = 1;
                    pmxMorph.Kind = PmxMorph.OffsetKind.Vertex;
                    for (int index2 = 0; index2 < vector3Array1.Length; ++index2)
                    {
                        PmxVertexMorph pmxVertexMorph = new PmxVertexMorph(num + index2, new Vector3((float)-vector3Array1[index2].x, (float)vector3Array1[index2].y, (float)-vector3Array1[index2].z));
                        pmxVertexMorph.Offset *= (float)this.scale;
                        pmxMorph.OffsetList.Add((PmxBaseMorph)pmxVertexMorph);
                    }
                    this.pmxFile.MorphList.Add(pmxMorph);
                }
            }
        }
        foreach (FBSTargetInfo fbsTargetInfo in (FBSTargetInfo[])((FBSBase)((ChaInfo)instance).mouthCtrl).FBSTarget)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = fbsTargetInfo.GetSkinnedMeshRenderer();
            int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
            Vector3[] vector3Array4 = new Vector3[skinnedMeshRenderer.sharedMesh.vertices.Length];
            Vector3[] vector3Array5 = new Vector3[skinnedMeshRenderer.sharedMesh.normals.Length];
            Vector3[] vector3Array6 = new Vector3[skinnedMeshRenderer.sharedMesh.tangents.Length];
            int num = 0;
            for (int index = 0; index < this.vertics_num.Length && this.vertics_num[index] != vector3Array4.Length; ++index)
                num += this.vertics_num[index];
            if (num < this.pmxFile.VertexList.Count)
            {
                for (int index3 = 0; index3 < blendShapeCount; ++index3)
                {
                    skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameVertices(index3, 0, vector3Array4, vector3Array5, vector3Array6);
                    PmxMorph pmxMorph = new PmxMorph();
                    pmxMorph.Name = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(index3);
                    pmxMorph.NameE = "";
                    pmxMorph.Panel = 1;
                    pmxMorph.Kind = PmxMorph.OffsetKind.Vertex;
                    for (int index4 = 0; index4 < vector3Array4.Length; ++index4)
                    {
                        PmxVertexMorph pmxVertexMorph = new PmxVertexMorph(num + index4, new Vector3((float)-vector3Array4[index4].x, (float)vector3Array4[index4].y, (float)-vector3Array4[index4].z));
                        pmxVertexMorph.Offset *= (float)this.scale;
                        pmxMorph.OffsetList.Add((PmxBaseMorph)pmxVertexMorph);
                    }
                    bool flag = true;
                    for (int index5 = 0; index5 < this.pmxFile.MorphList.Count; ++index5)
                    {
                        if (this.pmxFile.MorphList[index5].Name.Equals(pmxMorph.Name))
                            flag = false;
                    }
                    if (flag)
                        this.pmxFile.MorphList.Add(pmxMorph);
                }
            }
        }
        foreach (FBSTargetInfo fbsTargetInfo in (FBSTargetInfo[])((FBSBase)((ChaInfo)instance).eyebrowCtrl).FBSTarget)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = fbsTargetInfo.GetSkinnedMeshRenderer();
            int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
            Vector3[] vector3Array7 = new Vector3[skinnedMeshRenderer.sharedMesh.vertices.Length];
            Vector3[] vector3Array8 = new Vector3[skinnedMeshRenderer.sharedMesh.normals.Length];
            Vector3[] vector3Array9 = new Vector3[skinnedMeshRenderer.sharedMesh.tangents.Length];
            int num = 0;
            for (int index = 0; index < this.vertics_num.Length && this.vertics_num[index] != vector3Array7.Length; ++index)
                num += this.vertics_num[index];
            if (num < this.pmxFile.VertexList.Count)
            {
                for (int index6 = 0; index6 < blendShapeCount; ++index6)
                {
                    skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameVertices(index6, 0, vector3Array7, vector3Array8, vector3Array9);
                    PmxMorph pmxMorph = new PmxMorph();
                    pmxMorph.Name = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(index6);
                    pmxMorph.NameE = "";
                    pmxMorph.Panel = 1;
                    pmxMorph.Kind = PmxMorph.OffsetKind.Vertex;
                    for (int index7 = 0; index7 < vector3Array7.Length; ++index7)
                    {
                        PmxVertexMorph pmxVertexMorph = new PmxVertexMorph(num + index7, new Vector3((float)-vector3Array7[index7].x, (float)vector3Array7[index7].y, (float)-vector3Array7[index7].z));
                        pmxVertexMorph.Offset *= (float)this.scale;
                        pmxMorph.OffsetList.Add((PmxBaseMorph)pmxVertexMorph);
                    }
                    bool flag = true;
                    for (int index8 = 0; index8 < this.pmxFile.MorphList.Count; ++index8)
                    {
                        if (this.pmxFile.MorphList[index8].Name.Equals(pmxMorph.Name))
                            flag = false;
                    }
                    if (flag)
                        this.pmxFile.MorphList.Add(pmxMorph);
                }
            }
        }
    }

    private void setboneparent()
    {
    }

    private void setmaterial()
    {
        for (int index = 0; index < this.pmxFile.MaterialList.Count; ++index)
        {
            this.pmxFile.MaterialList[index].SetFlag(PmxMaterial.MaterialFlags.Edge, true);
            if (this.pmxFile.MaterialList[index].Name.Contains("hair") || this.pmxFile.MaterialList[index].Name.Contains("ahoge") || this.pmxFile.MaterialList[index].Name.Contains("mayuge"))
            {
                this.pmxFile.MaterialList[index].Ambient = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                this.pmxFile.MaterialList[index].Specular = new Color(1f, 1f, 1f, 0.0f);
            }
            else
            {
                this.pmxFile.MaterialList[index].Diffuse = new Color(1f, 1f, 1f, 1f);
                this.pmxFile.MaterialList[index].Specular = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                this.pmxFile.MaterialList[index].Ambient = new Color(1f, 1f, 1f, 1f);
                if (this.pmxFile.MaterialList[index].Name.Contains("shadowcast"))
                {
                    this.pmxFile.MaterialList[index].Diffuse = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    this.pmxFile.MaterialList[index].SetFlag(PmxMaterial.MaterialFlags.Edge, false);
                }
            }
        }
    }

    private void setSkinnedMeshList()
    {
        this.skinnedMeshList = new List<SkinnedMeshRenderer>();
        SkinnedMeshRenderer[] objectsOfType = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];
        for (int index = 0; index < objectsOfType.Length; ++index)
            this.skinnedMeshList.Add(objectsOfType[index]);
        this.meshList = new List<MeshFilter>();
        foreach (MeshFilter meshFilter in Object.FindObjectsOfType(typeof(MeshFilter)) as MeshFilter[])
            this.meshList.Add(meshFilter);
        this.vertics_num = new int[objectsOfType.Length];
        this.vertics_name = new string[objectsOfType.Length];
        this.msg += "\n";
    }

    public void CreatePmxHeader()
    {
        PmxElementFormat f = new PmxElementFormat(1f);
        f.VertexSize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.VertexList.Count);
        int val1 = int.MinValue;
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
            val1 = Math.Max(val1, Math.Abs(this.pmxFile.BoneList[index].IK.LinkList.Count));
        int count = Math.Max(val1, this.pmxFile.BoneList.Count);
        f.BoneSize = PmxElementFormat.GetSignedBufSize(count);
        if (f.BoneSize < 2)
            f.BoneSize = 2;
        f.MorphSize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.MorphList.Count);
        f.MaterialSize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.MaterialList.Count);
        f.BodySize = PmxElementFormat.GetUnsignedBufSize(this.pmxFile.BodyList.Count);
        PmxHeader pmxHeader = new PmxHeader(2.1f);
        pmxHeader.FromElementFormat(f);
        this.pmxFile.Header = pmxHeader;
    }

    public void CreateModelInfo()
    {
        PmxModelInfo pmxModelInfo = new PmxModelInfo()
        {
            ModelName = "koikatu",
            ModelNameE = "",
            Comment = "exported koikatu"
        };
        pmxModelInfo.Comment = "";
        this.pmxFile.ModelInfo = pmxModelInfo;
    }

    private PmxVertex.BoneWeight[] ConvertBoneWeight(
      BoneWeight unityWeight,
      Transform[] bones)
    {
        PmxVertex.BoneWeight[] boneWeightArray = new PmxVertex.BoneWeight[4];
        if (((BoneWeight)ref unityWeight).boneIndex0 >= 0 && ((BoneWeight)ref unityWeight).boneIndex0 < bones.Length)
            boneWeightArray[0].Bone = this.sbi((bones[((BoneWeight)ref unityWeight).boneIndex0]).name);
        boneWeightArray[0].Value = ((BoneWeight)ref unityWeight).weight0;
        if (((BoneWeight)ref unityWeight).boneIndex1 >= 0 && ((BoneWeight)ref unityWeight).boneIndex0 < bones.Length)
            boneWeightArray[1].Bone = this.sbi((bones[((BoneWeight)ref unityWeight).boneIndex1]).name);
        boneWeightArray[1].Value = ((BoneWeight)ref unityWeight).weight1;
        if (((BoneWeight)ref unityWeight).boneIndex2 >= 0 && ((BoneWeight)ref unityWeight).boneIndex0 < bones.Length)
            boneWeightArray[2].Bone = this.sbi((bones[((BoneWeight)ref unityWeight).boneIndex2]).name);
        boneWeightArray[2].Value = ((BoneWeight)ref unityWeight).weight2;
        if (((BoneWeight)ref unityWeight).boneIndex3 >= 0 && ((BoneWeight)ref unityWeight).boneIndex0 < bones.Length)
            boneWeightArray[3].Bone = this.sbi((bones[((BoneWeight)ref unityWeight).boneIndex3]).name);
        boneWeightArray[3].Value = ((BoneWeight)ref unityWeight).weight3;
        return boneWeightArray;
    }

    private void AddFaceList(int[] faceList, int count)
    {
        for (int index = 0; index < faceList.Length; ++index)
        {
            faceList[index] += count;
            this.pmxFile.FaceList.Add(faceList[index]);
        }
    }

    private Vector3 MultiplyVec3s(Vector3 v1, Vector3 v2) => new Vector3((float)(v1.x * v2.x), (float)(v1.y * v2.y), (float)(v1.z * v2.z));

    private Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Quaternion angle) => Vector3.op_Addition(Quaternion.op_Multiply(angle, Vector3.op_Subtraction(point, pivot)), pivot);

    public void CreateMeshList()
    {
        SkinnedMeshRenderer[] componentsInChildren = ((Component)GameObject.Find("BodyTop").transform).GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
        {
            this.vertics_num[index1] = componentsInChildren[index1].sharedMesh.vertices.Length;
            this.vertics_name[index1] = (((Renderer)componentsInChildren[index1]).sharedMaterial).name;
            GameObject gameObject = ((Component)componentsInChildren[index1]).gameObject;
            BoneWeight[] boneWeights = componentsInChildren[index1].sharedMesh.boneWeights;
            Mesh mesh1 = new Mesh();
            componentsInChildren[index1].BakeMesh(mesh1);
            Mesh mesh2 = mesh1;
            Vector2[] uv = mesh2.uv;
            Vector2[] uv2 = mesh2.uv2;
            Vector3[] normals = mesh2.normals;
            Vector3[] vertices = mesh2.vertices;
            for (int index2 = 0; index2 < mesh2.subMeshCount; ++index2)
            {
                int[] triangles = mesh2.GetTriangles(index2);
                this.AddFaceList(triangles, this.vertexCount);
                this.CreateMaterial(((Renderer)componentsInChildren[index1]).sharedMaterials[index2], triangles.Length);
            }
            this.vertexCount += mesh2.vertexCount;
            for (int index3 = 0; index3 < mesh2.vertexCount; ++index3)
            {
                PmxVertex pmxVertex = new PmxVertex();
                pmxVertex.UV = new Vector2((float)uv[index3].x, (float)(-uv[index3].y + 1.0));
                pmxVertex.Weight = this.ConvertBoneWeight(boneWeights[index3], componentsInChildren[index1].bones);
                Vector3 vector3_1 = this.RotateAroundPoint(normals[index3], Vector3.zero, gameObject.transform.rotation);
                pmxVertex.Normal = new Vector3((float)-vector3_1.x, (float)vector3_1.y, (float)-vector3_1.z);
                Vector3 vector3_2 = Vector3.op_Addition(this.RotateAroundPoint(vertices[index3], Vector3.zero, gameObject.transform.rotation), gameObject.transform.position);
                pmxVertex.Position = new Vector3((float)-vector3_2.x * (float)this.scale, (float)vector3_2.y * (float)this.scale, (float)-vector3_2.z * (float)this.scale);
                pmxVertex.Deform = PmxVertex.DeformType.BDEF4;
                this.pmxFile.VertexList.Add(pmxVertex);
            }
        }
    }

    private Vector3 TransToParent(Vector3 v, int index)
    {
        Transform bone = this.boneList[index];
        int index1 = -1;
        if (this.bonesMap.ContainsKey(bone.parent))
            index1 = this.bonesMap[bone.parent];
        if (index1 != -1)
        {
            Matrix4x4 matrix4x4_1 = new Matrix4x4();
            Matrix4x4 bindpose = this.bindposeList[index];
            Matrix4x4 worldToLocalMatrix = this.boneList[index1].worldToLocalMatrix;
            Matrix4x4 inverse = ((Matrix4x4)ref worldToLocalMatrix).inverse;
            Matrix4x4 matrix4x4_2 = Matrix4x4.op_Multiply(bindpose, inverse);
            v = ((Matrix4x4)ref matrix4x4_2).MultiplyVector(v);
            v = this.TransToParent(v, index1);
        }
        return v;
    }

    private Vector3 CalcPostion(Vector3 v, BoneWeight boneWeight, Transform[] bones)
    {
        Transform bone = bones[((BoneWeight)ref boneWeight).boneIndex0];
        if (this.bonesMap.ContainsKey(bone))
        {
            int bones1 = this.bonesMap[bone];
            v = this.TransToParent(v, bones1);
        }
        return v;
    }

    public void CreateMaterial(Material material, int count)
    {
        PmxMaterial pmxMaterial = new PmxMaterial();
        pmxMaterial.Name = (material).name;
        pmxMaterial.NameE = (material).name;
        pmxMaterial.Flags = PmxMaterial.MaterialFlags.DrawBoth | PmxMaterial.MaterialFlags.Shadow | PmxMaterial.MaterialFlags.SelfShadowMap | PmxMaterial.MaterialFlags.SelfShadow;
        if (Object.op_Inequality(material.mainTexture, null))
        {
            string str = (material).name;
            if (str.Contains("Instance"))
                str = str + "_(" + (object)(material).GetInstanceID() + ")";
            pmxMaterial.Tex = str + ".png";
            Texture mainTexture = material.mainTexture;
            TextureWriter.WriteTexture2D(this.pass + pmxMaterial.Tex, mainTexture);
        }
        if (material.HasProperty("_Color"))
            pmxMaterial.Diffuse = material.GetColor("_Color");
        if (material.HasProperty("_AmbColor"))
            pmxMaterial.Ambient = material.GetColor("_AmbColor");
        if (material.HasProperty("_Opacity"))
            pmxMaterial.Diffuse.a = (__Null)(double)material.GetFloat("_Opacity");
        if (material.HasProperty("_SpecularColor"))
            pmxMaterial.Specular = material.GetColor("_SpecularColor");
        if (!material.HasProperty("_Shininess"))
            ;
        if (material.HasProperty("_OutlineColor"))
        {
            pmxMaterial.EdgeSize = material.GetFloat("_OutlineWidth");
            pmxMaterial.EdgeColor = material.GetColor("_OutlineColor");
        }
        pmxMaterial.FaceCount = count;
        this.pmxFile.MaterialList.Add(pmxMaterial);
    }

    public void CreateBoneList()
    {
        Transform transform = GameObject.Find("BodyTop").transform;
        List<Transform> transformList = new List<Transform>();
        Transform[] componentsInChildren = ((Component)transform).GetComponentsInChildren<Transform>();
        for (int index = 0; index < componentsInChildren.Length; ++index)
        {
            PmxBone pmxBone = new PmxBone();
            pmxBone.Name = (componentsInChildren[index]).name;
            pmxBone.Parent = transformList.IndexOf(componentsInChildren[index].parent);
            Vector3 vector3 = Vector3.op_Multiply(((Component)componentsInChildren[index]).transform.position, (float)this.scale);
            pmxBone.Position = new Vector3((float)-vector3.x, (float)vector3.y, (float)-vector3.z);
            transformList.Add(componentsInChildren[index]);
            this.pmxFile.BoneList.Add(pmxBone);
        }
    }

    public void Save() => this.pmxFile.ToFile(this.pass + "model.pmx");

    private int serchbone(string name)
    {
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (this.pmxFile.BoneList[index].Name == name)
                return index;
        }
        return 0;
    }

    public void sortmaterial()
    {
        int num = 0;
        List<int>[] intListArray = new List<int>[this.pmxFile.MaterialList.Count];
        for (int index1 = 0; index1 < this.pmxFile.MaterialList.Count; ++index1)
        {
            PmxMaterial material = this.pmxFile.MaterialList[index1];
            if (material.Name.Contains("SkinHi"))
                num = index1;
            intListArray[index1] = new List<int>();
            for (int index2 = 0; index2 < material.FaceCount; ++index2)
                intListArray[index1].Add(this.pmxFile.FaceList[index2]);
            this.pmxFile.FaceList.RemoveRange(0, material.FaceCount);
        }
        if (num != 0 && num - 2 >= 0)
        {
            int index = num;
            List<int> intList = intListArray[index];
            intListArray[index] = intListArray[index - 2];
            intListArray[index - 2] = intList;
            PmxMaterial material = this.pmxFile.MaterialList[index];
            this.pmxFile.MaterialList[index] = this.pmxFile.MaterialList[index - 2];
            this.pmxFile.MaterialList[index - 2] = material;
        }
        for (int index3 = intListArray.Length - 1; index3 >= 0; --index3)
        {
            for (int index4 = 0; index4 < intListArray[index3].Count; ++index4)
                this.pmxFile.FaceList.Add(intListArray[index3][index4]);
        }
        for (int index = 0; index < this.pmxFile.MaterialList.Count; ++index)
        {
            PmxMaterial material = this.pmxFile.MaterialList[index];
            this.pmxFile.MaterialList.RemoveAt(index);
            this.pmxFile.MaterialList.Insert(0, material);
        }
    }

    private void setParent()
    {
        for (int index1 = 0; index1 < this.skinnedMeshList.Count; ++index1)
        {
            SkinnedMeshRenderer skinnedMesh = this.skinnedMeshList[index1];
            for (int index2 = 0; index2 < skinnedMesh.bones.Length; ++index2)
            {
                PmxBone pmxBone = this.serchBone((skinnedMesh.bones[index2]).name);
                if (pmxBone != null)
                {
                    int num = this.serchBonei((skinnedMesh.bones[index2].parent).name);
                    pmxBone.Parent = num;
                }
            }
        }
    }

    private int serchBonei(string name)
    {
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (this.pmxFile.BoneList[index].Name == name)
                return index;
        }
        return -1;
    }

    private PmxBone serchBone(string name)
    {
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (this.pmxFile.BoneList[index].Name == name)
                return this.pmxFile.BoneList[index];
        }
        return (PmxBone)null;
    }

    private void sortbone(int boneindex, int index)
    {
        for (int index1 = 0; index1 < this.pmxFile.VertexList.Count; ++index1)
        {
            for (int index2 = 0; index2 < this.pmxFile.VertexList[index1].Weight.Length; ++index2)
            {
                if (index >= this.pmxFile.VertexList[index1].Weight[index2].Bone && this.pmxFile.VertexList[index1].Weight[index2].Bone > boneindex)
                    --this.pmxFile.VertexList[index1].Weight[index2].Bone;
                else if (this.pmxFile.VertexList[index1].Weight[index2].Bone == boneindex)
                    this.pmxFile.VertexList[index1].Weight[index2].Bone = index;
            }
        }
        for (int index3 = 0; index3 < this.pmxFile.BoneList.Count; ++index3)
        {
            if (index >= this.pmxFile.BoneList[index3].Parent && this.pmxFile.BoneList[index3].Parent > boneindex)
                --this.pmxFile.BoneList[index3].Parent;
            else if (this.pmxFile.BoneList[index3].Parent == boneindex)
                this.pmxFile.BoneList[index3].Parent = index;
            if (index >= this.pmxFile.BoneList[index3].To_Bone && this.pmxFile.BoneList[index3].To_Bone > boneindex)
                --this.pmxFile.BoneList[index3].To_Bone;
            else if (this.pmxFile.BoneList[index3].To_Bone == boneindex)
                this.pmxFile.BoneList[index3].To_Bone = index;
        }
        PmxBone bone = this.pmxFile.BoneList[boneindex];
        this.pmxFile.BoneList.RemoveAt(boneindex);
        this.pmxFile.BoneList.Insert(index, bone);
    }

    public void addbone()
    {
        this.insertbone(0, new PmxBone()
        {
            Name = "全ての親",
            Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible
        });
        this.insertbone(1, new PmxBone()
        {
            Name = "センター",
            Position = new Vector3(0.0f, this.sb("下半身").Position.Y * 0.75f, 0.0f),
            Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible
        });
        this.sb("BodyTop").Parent = this.sbi("センター");
        if (this.sb("上半身") != null)
        {
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("左足首")) + 1, new PmxBone()
            {
                Name = "左つま先"
            });
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("左つま先")) + 1, new PmxBone()
            {
                Name = "左足ＩＫ"
            });
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("左足ＩＫ")) + 1, new PmxBone()
            {
                Name = "左つま先ＩＫ"
            });
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("右足首")) + 1, new PmxBone()
            {
                Name = "右つま先"
            });
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("右つま先")) + 1, new PmxBone()
            {
                Name = "右足ＩＫ"
            });
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("右足ＩＫ")) + 1, new PmxBone()
            {
                Name = "右つま先ＩＫ"
            });
        }
        if (this.sb("頭") != null)
            this.insertbone(this.pmxFile.BoneList.IndexOf(this.sb("頭")) + 1, new PmxBone()
            {
                Name = "両目x"
            });
        if (this.sb("上半身") != null)
        {
            this.sb("左足首").Parent = this.sbi("左ひざ");
            this.sb("右足首").Parent = this.sbi("右ひざ");
            PmxBone pmxBone1 = this.sb("左つま先");
            pmxBone1.Position = this.sb("cf_j_toes_L").Position;
            pmxBone1.To_Bone = this.sb("cf_j_toes_L").To_Bone;
            pmxBone1.Flags = PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
            pmxBone1.Parent = this.sbi("左足首");
            PmxBone pmxBone2 = this.sb("左足ＩＫ");
            pmxBone2.Position = this.sb("左足首").Position;
            pmxBone2.To_Offset = new Vector3(0.0f, -1.3f, 0.0f);
            pmxBone2.Parent = this.sbi("全ての親");
            pmxBone2.Flags = PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK;
            pmxBone2.IK.Angle = 2f;
            pmxBone2.IK.LoopCount = 40;
            pmxBone2.IK.Target = this.sbi("左足首");
            pmxBone2.IK.LinkList.Add(new PmxIK.IKLink()
            {
                Bone = this.sbi("左ひざ"),
                IsLimit = true,
                High = new Vector3(-1f * (float)Math.PI / 360f, 0.0f, 0.0f),
                Low = new Vector3(-3.141593f, 0.0f, 0.0f)
            });
            pmxBone2.IK.LinkList.Add(new PmxIK.IKLink()
            {
                Bone = this.sbi("左足")
            });
            PmxBone pmxBone3 = this.sb("左つま先ＩＫ");
            pmxBone3.Position = this.sb("左つま先").Position;
            pmxBone3.To_Offset = new Vector3(0.0f, -1.3081f, 0.0f);
            pmxBone3.Parent = this.sbi("左足ＩＫ");
            pmxBone3.Flags = PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK;
            pmxBone3.IK.Angle = 4f;
            pmxBone3.IK.LoopCount = 3;
            pmxBone3.IK.Target = this.sbi("左つま先");
            pmxBone3.IK.LinkList.Add(new PmxIK.IKLink()
            {
                Bone = this.sbi("左足首")
            });
            PmxBone pmxBone4 = this.sb("右つま先");
            pmxBone4.Position = this.sb("cf_j_toes_R").Position;
            pmxBone4.To_Bone = this.sb("cf_j_toes_R").To_Bone;
            pmxBone4.Flags = PmxBone.BoneFlags.ToBone | PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable;
            pmxBone4.Parent = this.sbi("右足首");
            PmxBone pmxBone5 = this.sb("右足ＩＫ");
            pmxBone5.Position = this.sb("右足首").Position;
            pmxBone5.To_Offset = new Vector3(0.0f, -1.3f, 0.0f);
            pmxBone5.Parent = this.sbi("全ての親");
            pmxBone5.Flags = PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK;
            pmxBone5.IK.Angle = 2f;
            pmxBone5.IK.LoopCount = 40;
            pmxBone5.IK.Target = this.sbi("右足首");
            pmxBone5.IK.LinkList.Add(new PmxIK.IKLink()
            {
                Bone = this.sbi("右ひざ"),
                IsLimit = true,
                High = new Vector3(-1f * (float)Math.PI / 360f, 0.0f, 0.0f),
                Low = new Vector3(-3.141593f, 0.0f, 0.0f)
            });
            pmxBone5.IK.LinkList.Add(new PmxIK.IKLink()
            {
                Bone = this.sbi("右足")
            });
            PmxBone pmxBone6 = this.sb("右つま先ＩＫ");
            pmxBone6.Position = this.sb("右つま先").Position;
            pmxBone6.To_Offset = new Vector3(0.0f, -1.3081f, 0.0f);
            pmxBone6.Parent = this.sbi("右足ＩＫ");
            pmxBone6.Flags = PmxBone.BoneFlags.Translation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.IK;
            pmxBone6.IK.Angle = 4f;
            pmxBone6.IK.LoopCount = 3;
            pmxBone6.IK.Target = this.sbi("右つま先");
            pmxBone6.IK.LinkList.Add(new PmxIK.IKLink()
            {
                Bone = this.sbi("右足首")
            });
        }
        if (this.sb("頭") == null)
            return;
        PmxBone pmxBone = this.sb("両目x");
        pmxBone.Position = new Vector3(0.0f, this.sb("頭").Position.Y + 3f, 0.0f);
        pmxBone.To_Offset = new Vector3(0.0f, 0.0f, -1f);
        pmxBone.Parent = this.sbi("頭");
    }

    private void insertbone(int index, PmxBone b)
    {
        for (int index1 = 0; index1 < this.pmxFile.VertexList.Count; ++index1)
        {
            for (int index2 = 0; index2 < this.pmxFile.VertexList[index1].Weight.Length; ++index2)
            {
                if (this.pmxFile.VertexList[index1].Weight[index2].Bone >= index)
                    ++this.pmxFile.VertexList[index1].Weight[index2].Bone;
            }
        }
        for (int index3 = 0; index3 < this.pmxFile.BoneList.Count; ++index3)
        {
            if (this.pmxFile.BoneList[index3].Parent >= index)
                ++this.pmxFile.BoneList[index3].Parent;
            if (this.pmxFile.BoneList[index3].To_Bone >= index)
                ++this.pmxFile.BoneList[index3].To_Bone;
        }
        this.pmxFile.BoneList.Insert(index, b);
    }

    private PmxBone sb(string name)
    {
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (this.pmxFile.BoneList[index].Name.Equals(name))
                return this.pmxFile.BoneList[index];
        }
        return (PmxBone)null;
    }

    private void cn(PmxBone b, string name)
    {
        if (b == null)
            return;
        b.Name = name;
    }

    private int sbi(string name)
    {
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (this.pmxFile.BoneList[index].Name == name)
                return index;
        }
        return -1;
    }

    private void sortbone2(int boneindex, int index)
    {
        for (int index1 = 0; index1 < this.pmxFile.VertexList.Count; ++index1)
        {
            for (int index2 = 0; index2 < this.pmxFile.VertexList[index1].Weight.Length; ++index2)
            {
                if (index <= this.pmxFile.VertexList[index1].Weight[index2].Bone && this.pmxFile.VertexList[index1].Weight[index2].Bone < boneindex)
                    ++this.pmxFile.VertexList[index1].Weight[index2].Bone;
                else if (this.pmxFile.VertexList[index1].Weight[index2].Bone == boneindex)
                    this.pmxFile.VertexList[index1].Weight[index2].Bone = index;
            }
        }
        for (int index3 = 0; index3 < this.pmxFile.BoneList.Count; ++index3)
        {
            if (index <= this.pmxFile.BoneList[index3].Parent && this.pmxFile.BoneList[index3].Parent < boneindex)
                ++this.pmxFile.BoneList[index3].Parent;
            else if (this.pmxFile.BoneList[index3].Parent == boneindex)
                this.pmxFile.BoneList[index3].Parent = index;
            if (index <= this.pmxFile.BoneList[index3].To_Bone && this.pmxFile.BoneList[index3].To_Bone < boneindex)
                ++this.pmxFile.BoneList[index3].To_Bone;
            else if (this.pmxFile.BoneList[index3].To_Bone == boneindex)
                this.pmxFile.BoneList[index3].To_Bone = index;
        }
        PmxBone bone = this.pmxFile.BoneList[boneindex];
        this.pmxFile.BoneList.RemoveAt(boneindex);
        this.pmxFile.BoneList.Insert(index, bone);
    }

    private void changeboneinfo()
    {
        if (this.sb("頭") != null)
        {
            PmxBone pmxBone1 = this.sb("左目x");
            if (pmxBone1 != null)
            {
                pmxBone1.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.AppendRotation;
                pmxBone1.AppendRatio = 0.5f;
                pmxBone1.AppendParent = this.sbi("両目x");
            }
            PmxBone pmxBone2 = this.sb("右目x");
            if (pmxBone2 != null)
            {
                pmxBone2.Flags = PmxBone.BoneFlags.Rotation | PmxBone.BoneFlags.Visible | PmxBone.BoneFlags.Enable | PmxBone.BoneFlags.AppendRotation;
                pmxBone2.AppendRatio = 0.5f;
                pmxBone2.AppendParent = this.sbi("両目x");
            }
        }
        this.sb("センター").Parent = this.sbi("全ての親");
    }

    private PmxMorph sm(string name)
    {
        for (int index = 0; index < this.pmxFile.MorphList.Count; ++index)
        {
            if (this.pmxFile.MorphList[index].Name.Equals(name))
                return this.pmxFile.MorphList[index];
        }
        return (PmxMorph)null;
    }

    private int smi(string name)
    {
        for (int index = 0; index < this.pmxFile.MorphList.Count; ++index)
        {
            if (this.pmxFile.MorphList[index].Name.Equals(name))
                return index;
        }
        return -1;
    }

    public void addphysics()
    {
        List<PmxBone> pmxBoneList1 = new List<PmxBone>();
        List<PmxBone> pmxBoneList2 = new List<PmxBone>();
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (!this.pmxFile.BoneList[index].Name.Contains("top") && this.pmxFile.BoneList[index].Name.Contains("cf_J_hair"))
                pmxBoneList1.Add(this.pmxFile.BoneList[index]);
            if (this.pmxFile.BoneList[index].Name.Contains("cf_j_sk"))
                pmxBoneList2.Add(this.pmxFile.BoneList[index]);
        }
        for (int index1 = 0; index1 < pmxBoneList1.Count; ++index1)
        {
            if (pmxBoneList1[index1].Parent != -1)
            {
                PmxBone bone = this.pmxFile.BoneList[pmxBoneList1[index1].Parent];
                Vector3 vector3_1 = new Vector3();
                int index2 = this.serchchild(pmxBoneList1[index1].Name);
                Vector3 vector3_2 = index2 == -1 ? new Vector3(0.0f, -0.5f, 0.0f) + pmxBoneList1[index1].Position : this.pmxFile.BoneList[index2].Position;
                PmxBody pmxBody = new PmxBody();
                pmxBody.Name = pmxBoneList1[index1].Name;
                pmxBody.Bone = this.sbi(pmxBoneList1[index1].Name);
                pmxBody.Position = (pmxBoneList1[index1].Position + vector3_2) / 2f;
                pmxBody.BoxType = PmxBody.BoxKind.Capsule;
                pmxBody.Group = 2;
                pmxBody.Mass = 1f;
                pmxBody.PositionDamping = 0.9f;
                pmxBody.RotationDamping = 0.99f;
                PmxBodyPassGroup pmxBodyPassGroup = new PmxBodyPassGroup();
                pmxBodyPassGroup.Flags[2] = true;
                pmxBody.PassGroup = pmxBodyPassGroup;
                pmxBody.Mode = PmxBody.ModeType.Dynamic;
                if (bone.Name.Equals("cf_J_hairF_top") || bone.Name.Equals("cf_J_hairB_top"))
                    pmxBody.Mode = PmxBody.ModeType.Static;
                pmxBody.BoxSize = new Vector3(0.2f, this.getDistance(pmxBoneList1[index1].Position, vector3_2) / 2f, 0.0f);
                pmxBody.Rotation = this.getDirection(pmxBoneList1[index1].Position, vector3_2);
                this.pmxFile.BodyList.Add(pmxBody);
                if (this.sbdi(bone.Name) == -1)
                    pmxBody.Mode = PmxBody.ModeType.Static;
                else if (!bone.Name.Equals("cf_J_hairF_top") && !bone.Name.Equals("cf_J_hairB_top"))
                    this.pmxFile.JointList.Add(new PmxJoint()
                    {
                        Name = pmxBoneList1[index1].Name,
                        Position = pmxBoneList1[index1].Position,
                        Rotation = pmxBody.Rotation,
                        BodyA = this.sbdi(bone.Name),
                        BodyB = this.sbdi(pmxBody.Name),
                        Limit_AngleLow = new Vector3(-0.1745329f, -1f * (float)Math.PI / 36f, -0.1745329f),
                        Limit_AngleHigh = new Vector3(0.1745329f, (float)Math.PI / 36f, 0.1745329f)
                    });
            }
        }
        PmxBone pmxBone1 = this.sb("下半身");
        PmxBody pmxBody1 = new PmxBody();
        pmxBody1.Name = pmxBone1.Name;
        pmxBody1.Bone = this.sbi(pmxBone1.Name);
        pmxBody1.Position = new Vector3(0.0f, this.sb("下半身").Position.y, 0.6f);
        Quaternion.Euler(0.0f, 0.0f, 1.570796f);
        pmxBody1.Rotation = new Vector3(0.0f, 0.0f, 1.570796f);
        pmxBody1.BoxType = PmxBody.BoxKind.Capsule;
        pmxBody1.BoxSize = new Vector3(1f, 1.3f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody1);
        for (int index3 = 0; index3 < pmxBoneList2.Count; ++index3)
        {
            PmxBone bone = this.pmxFile.BoneList[pmxBoneList2[index3].Parent];
            Vector3 vector3_3 = new Vector3();
            int index4 = this.serchchild(pmxBoneList2[index3].Name);
            Vector3 vector3_4 = index4 == -1 ? new Vector3(0.0f, -0.5f, 0.0f) + pmxBoneList2[index3].Position : this.pmxFile.BoneList[index4].Position;
            PmxBody pmxBody2 = new PmxBody();
            pmxBody2.Name = pmxBoneList2[index3].Name;
            pmxBody2.Bone = this.sbi(pmxBoneList2[index3].Name);
            pmxBody2.Position = (pmxBoneList2[index3].Position + vector3_4) / 2f;
            pmxBody2.BoxType = PmxBody.BoxKind.Capsule;
            pmxBody2.Group = 3;
            pmxBody2.Mass = 1f;
            pmxBody2.PositionDamping = 0.99f;
            pmxBody2.RotationDamping = 0.99f;
            PmxBodyPassGroup pmxBodyPassGroup = new PmxBodyPassGroup();
            pmxBodyPassGroup.Flags[3] = true;
            pmxBody2.PassGroup = pmxBodyPassGroup;
            pmxBody2.Mode = PmxBody.ModeType.Dynamic;
            if (bone.Name == "cf_s_waist01" || bone.Name == "下半身" || bone.Name.Contains("cf_d_sk"))
                pmxBody2.Mode = PmxBody.ModeType.Static;
            pmxBody2.BoxSize = new Vector3(0.2f, this.getDistance(pmxBoneList2[index3].Position, vector3_4) / 2f, 0.0f);
            pmxBody2.Rotation = this.getDirection(pmxBoneList2[index3].Position, vector3_4);
            this.pmxFile.BodyList.Add(pmxBody2);
            if (!(bone.Name == "cf_s_waist01") && !(bone.Name == "下半身") && !bone.Name.Contains("cf_d_sk"))
                this.pmxFile.JointList.Add(new PmxJoint()
                {
                    Name = pmxBoneList2[index3].Name,
                    Position = pmxBoneList2[index3].Position,
                    Rotation = pmxBody2.Rotation,
                    BodyA = this.sbdi(bone.Name),
                    BodyB = this.sbdi(pmxBody2.Name),
                    Limit_AngleLow = new Vector3(-0.5235988f, -0.2617994f, -0.5235988f),
                    Limit_AngleHigh = new Vector3(0.5235988f, 0.2617994f, 0.5235988f)
                });
        }
        int num1 = 0;
        char ch;
        for (int index = 0; index < pmxBoneList2.Count; ++index)
        {
            ch = pmxBoneList2[index].Name[9];
            int num2 = int.Parse(ch.ToString() ?? "");
            if (num1 < num2)
                num1 = num2;
        }
        for (int index5 = 0; index5 < pmxBoneList2.Count; ++index5)
        {
            ch = pmxBoneList2[index5].Name[9];
            int num3 = int.Parse(ch.ToString() ?? "");
            ch = pmxBoneList2[index5].Name[12];
            int num4 = int.Parse(ch.ToString() ?? "");
            PmxBone pmxBone2 = (PmxBone)null;
            int num5 = 0;
            if (num3 != num1)
                num5 = num3 + 1;
            for (int index6 = 0; index6 < pmxBoneList2.Count; ++index6)
            {
                ch = pmxBoneList2[index6].Name[9];
                if (int.Parse(ch.ToString() ?? "") == num5)
                {
                    ch = pmxBoneList2[index6].Name[12];
                    if (int.Parse(ch.ToString() ?? "") == num4)
                        pmxBone2 = pmxBoneList2[index6];
                }
            }
            if (pmxBone2 != null)
                this.pmxFile.JointList.Add(new PmxJoint()
                {
                    Name = pmxBoneList2[index5].Name + "[side]",
                    Position = pmxBoneList2[index5].Position,
                    Rotation = this.pmxFile.BodyList[this.sbdi(pmxBoneList2[index5].Name)].Rotation,
                    BodyA = this.sbdi(pmxBoneList2[index5].Name),
                    BodyB = this.sbdi(pmxBone2.Name),
                    Limit_MoveLow = new Vector3(0.0f, 0.0f, 0.0f),
                    Limit_MoveHigh = new Vector3(0.0f, 0.0f, 0.0f),
                    Limit_AngleLow = new Vector3(-0.5235988f, -0.2617994f, -0.5235988f),
                    Limit_AngleHigh = new Vector3(0.5235988f, 0.2617994f, 0.5235988f)
                });
        }
        PmxBone pmxBone3 = this.sb("右足");
        pmxBone3.To_Bone = this.sbi("右ひざ");
        this.pmxFile.BodyList.Add(new PmxBody()
        {
            Name = pmxBone3.Name,
            Bone = this.sbi(pmxBone3.Name),
            Position = pmxBone3.To_Bone == -1 ? pmxBone3.Position + pmxBone3.To_Offset / 2f : (pmxBone3.Position + this.pmxFile.BoneList[pmxBone3.To_Bone].Position) / 2f,
            BoxType = PmxBody.BoxKind.Capsule,
            BoxSize = new Vector3(0.8f, (float)((double)this.getDistance(this.pmxFile.BoneList[pmxBone3.To_Bone].Position, pmxBone3.Position) / 2.0 * 1.5), 0.0f),
            Rotation = this.getDirection(this.pmxFile.BoneList[pmxBone3.To_Bone].Position, pmxBone3.Position)
        });
        PmxBone pmxBone4 = this.sb("左足");
        pmxBone4.To_Bone = this.sbi("左ひざ");
        this.pmxFile.BodyList.Add(new PmxBody()
        {
            Name = pmxBone4.Name,
            Bone = this.sbi(pmxBone4.Name),
            Position = pmxBone4.To_Bone == -1 ? pmxBone4.Position + pmxBone4.To_Offset / 2f : (pmxBone4.Position + this.pmxFile.BoneList[pmxBone4.To_Bone].Position) / 2f,
            BoxType = PmxBody.BoxKind.Capsule,
            BoxSize = new Vector3(0.8f, (float)((double)this.getDistance(this.pmxFile.BoneList[pmxBone4.To_Bone].Position, pmxBone4.Position) / 2.0 * 1.5), 0.0f),
            Rotation = this.getDirection(this.pmxFile.BoneList[pmxBone4.To_Bone].Position, pmxBone4.Position)
        });
    }

    private int serchchild(string name)
    {
        for (int index = 0; index < this.pmxFile.BoneList.Count; ++index)
        {
            if (this.pmxFile.BoneList[index].Parent != -1 && this.pmxFile.BoneList[this.pmxFile.BoneList[index].Parent].Name == name)
                return index;
        }
        return -1;
    }

    private int sbdi(string name)
    {
        for (int index = 0; index < this.pmxFile.BodyList.Count; ++index)
        {
            if (this.pmxFile.BodyList[index].Name == name)
                return index;
        }
        return -1;
    }

    private Vector3 getDirection(Vector3 first, Vector3 last)
    {
        Vector3 vector3 = new Vector3();
        float num = (float)Math.Sqrt(((double)last.X - (double)first.X) * ((double)last.X - (double)first.X) + ((double)last.Z - (double)first.Z) * ((double)last.Z - (double)first.Z));
        vector3.X = (float)(Math.Atan2((double)last.Y - (double)first.Y, (double)num) + Math.PI / 2.0);
        vector3.Y = -(float)(Math.Atan2((double)last.Z - (double)first.Z, (double)last.X - (double)first.X) + Math.PI / 2.0);
        return vector3;
    }

    private float getDistance(Vector3 one, Vector3 two) => (float)Math.Sqrt(((double)one.X - (double)two.X) * ((double)one.X - (double)two.X) + ((double)one.Y - (double)two.Y) * ((double)one.Y - (double)two.Y) + ((double)one.Z - (double)two.Z) * ((double)one.Z - (double)two.Z));

    private void addAccessory()
    {
        MeshFilter[] componentsInChildren = ((Component)GameObject.Find("BodyTop").transform).GetComponentsInChildren<MeshFilter>();
        for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
        {
            GameObject gameObject = ((Component)componentsInChildren[index1]).gameObject;
            Mesh sharedMesh = componentsInChildren[index1].sharedMesh;
            BoneWeight[] boneWeights = sharedMesh.boneWeights;
            Vector2[] uv = sharedMesh.uv;
            Vector2[] uv2 = sharedMesh.uv2;
            Vector3[] normals = sharedMesh.normals;
            Vector3[] vertices = sharedMesh.vertices;
            for (int index2 = 0; index2 < sharedMesh.subMeshCount; ++index2)
            {
                int[] triangles = sharedMesh.GetTriangles(index2);
                this.AddFaceList(triangles, this.vertexCount);
                this.CreateMaterial(((Renderer)(((Component)componentsInChildren[index1]).gameObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer)).sharedMaterials[index2], triangles.Length);
            }
            this.vertexCount += sharedMesh.vertexCount;
            PmxBone pmxBone = new PmxBone();
            pmxBone.Name = (componentsInChildren[index1]).name;
            pmxBone.Parent = this.sbi((componentsInChildren[index1]).name);
            Vector3 vector3_1 = Vector3.op_Multiply(((Component)componentsInChildren[index1]).transform.position, (float)this.scale);
            pmxBone.Position = new Vector3((float)-vector3_1.x, (float)vector3_1.y, (float)-vector3_1.z);
            this.pmxFile.BoneList.Add(pmxBone);
            for (int index3 = 0; index3 < sharedMesh.vertexCount; ++index3)
            {
                PmxVertex pmxVertex = new PmxVertex();
                pmxVertex.UV = new Vector2((float)uv[index3].x, (float)(-uv[index3].y + 1.0));
                pmxVertex.Weight = new PmxVertex.BoneWeight[4];
                pmxVertex.Weight[0].Bone = this.pmxFile.BoneList.Count - 1;
                pmxVertex.Weight[0].Value = 1f;
                Vector3 vector3_2 = this.RotateAroundPoint(normals[index3], Vector3.zero, gameObject.transform.rotation);
                pmxVertex.Normal = new Vector3((float)-vector3_2.x, (float)vector3_2.y, (float)-vector3_2.z);
                Vector3 vector3_3 = Vector3.op_Addition(this.RotateAroundPoint(this.MultiplyVec3s(vertices[index3], gameObject.transform.lossyScale), Vector3.zero, gameObject.transform.rotation), gameObject.transform.position);
                pmxVertex.Position = new Vector3((float)-vector3_3.x * (float)this.scale, (float)vector3_3.y * (float)this.scale, (float)-vector3_3.z * (float)this.scale);
                pmxVertex.Deform = PmxVertex.DeformType.BDEF4;
                this.pmxFile.VertexList.Add(pmxVertex);
            }
        }
    }

    public void phymune()
    {
        PmxBody pmxBody1 = new PmxBody();
        pmxBody1.Name = "左胸操作";
        pmxBody1.Bone = this.sbi("左胸操作");
        pmxBody1.Position = this.sb("左胸操作").Position;
        pmxBody1.BoxType = PmxBody.BoxKind.Box;
        pmxBody1.Group = 13;
        pmxBody1.Mass = 1f;
        pmxBody1.PositionDamping = 0.5f;
        pmxBody1.RotationDamping = 0.5f;
        PmxBodyPassGroup pmxBodyPassGroup1 = new PmxBodyPassGroup();
        pmxBodyPassGroup1.Flags[0] = true;
        pmxBodyPassGroup1.Flags[1] = true;
        pmxBodyPassGroup1.Flags[2] = true;
        pmxBodyPassGroup1.Flags[3] = true;
        pmxBodyPassGroup1.Flags[13] = true;
        pmxBody1.PassGroup = pmxBodyPassGroup1;
        pmxBody1.Mode = PmxBody.ModeType.Static;
        pmxBody1.BoxSize = new Vector3(0.1f, 0.1f, 0.1f);
        this.pmxFile.BodyList.Add(pmxBody1);
        PmxBody pmxBody2 = new PmxBody();
        pmxBody2.Name = "左AH1";
        pmxBody2.Bone = this.sbi("左AH1");
        pmxBody2.Position = this.sb("左AH1").Position;
        pmxBody2.BoxType = PmxBody.BoxKind.Sphere;
        pmxBody2.Group = 13;
        pmxBody2.Mass = 0.4f;
        pmxBody2.PositionDamping = 0.9999f;
        pmxBody2.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup2 = new PmxBodyPassGroup();
        pmxBodyPassGroup2.Flags[0] = true;
        pmxBodyPassGroup2.Flags[1] = true;
        pmxBodyPassGroup2.Flags[2] = true;
        pmxBodyPassGroup2.Flags[3] = true;
        pmxBodyPassGroup2.Flags[13] = true;
        pmxBody2.PassGroup = pmxBodyPassGroup2;
        pmxBody2.Mode = PmxBody.ModeType.Dynamic;
        pmxBody2.BoxSize = new Vector3(0.3f, 0.0f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody2);
        PmxBody pmxBody3 = new PmxBody();
        pmxBody3.Name = "左AH2";
        pmxBody3.Bone = this.sbi("左AH2");
        pmxBody3.Position = this.sb("左AH2").Position;
        pmxBody3.BoxType = PmxBody.BoxKind.Sphere;
        pmxBody3.Group = 13;
        pmxBody3.Mass = 0.1f;
        pmxBody3.PositionDamping = 0.9999f;
        pmxBody3.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup3 = new PmxBodyPassGroup();
        pmxBodyPassGroup3.Flags[0] = true;
        pmxBodyPassGroup3.Flags[1] = true;
        pmxBodyPassGroup3.Flags[2] = true;
        pmxBodyPassGroup3.Flags[3] = true;
        pmxBodyPassGroup3.Flags[13] = true;
        pmxBody3.PassGroup = pmxBodyPassGroup3;
        pmxBody3.Mode = PmxBody.ModeType.Dynamic;
        pmxBody3.BoxSize = new Vector3(0.3f, 0.0f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody3);
        PmxBody pmxBody4 = new PmxBody();
        pmxBody4.Name = "左胸操作接続";
        pmxBody4.Bone = -1;
        pmxBody4.Position = (this.sb("左AH2").Position + this.sb("左AH1").Position) / 2f;
        pmxBody4.BoxType = PmxBody.BoxKind.Capsule;
        pmxBody4.Group = 13;
        pmxBody4.Mass = 0.1f;
        pmxBody4.PositionDamping = 0.9999f;
        pmxBody4.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup4 = new PmxBodyPassGroup();
        pmxBodyPassGroup4.Flags[0] = true;
        pmxBodyPassGroup4.Flags[1] = true;
        pmxBodyPassGroup4.Flags[2] = true;
        pmxBodyPassGroup4.Flags[3] = true;
        pmxBodyPassGroup4.Flags[13] = true;
        pmxBody4.PassGroup = pmxBodyPassGroup4;
        pmxBody4.Mode = PmxBody.ModeType.Dynamic;
        pmxBody4.BoxSize = new Vector3(0.1f, 1.515f, 0.0f);
        pmxBody4.Rotation = new Vector3(0.0f, 1.570796f, -1.570796f);
        this.pmxFile.BodyList.Add(pmxBody4);
        PmxBody pmxBody5 = new PmxBody();
        pmxBody5.Name = "左胸操作衝突";
        pmxBody5.Bone = -1;
        pmxBody5.Position = new Vector3(this.sb("左胸操作").Position.X, this.sb("左胸操作").Position.Y, this.sb("左胸操作").Position.Z - 0.12f);
        pmxBody5.BoxType = PmxBody.BoxKind.Sphere;
        pmxBody5.Group = 7;
        pmxBody5.Mass = 0.1f;
        pmxBody5.PositionDamping = 0.9999f;
        pmxBody5.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup5 = new PmxBodyPassGroup();
        pmxBodyPassGroup5.Flags[0] = true;
        pmxBodyPassGroup5.Flags[13] = true;
        pmxBody5.PassGroup = pmxBodyPassGroup5;
        pmxBody5.Mode = PmxBody.ModeType.Dynamic;
        pmxBody5.BoxSize = new Vector3(0.4f, 0.0f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody5);
        PmxBody pmxBody6 = new PmxBody();
        pmxBody6.Name = "右胸操作";
        pmxBody6.Bone = this.sbi("右胸操作");
        pmxBody6.Position = this.sb("右胸操作").Position;
        pmxBody6.BoxType = PmxBody.BoxKind.Box;
        pmxBody6.Group = 13;
        pmxBody6.Mass = 1f;
        pmxBody6.PositionDamping = 0.5f;
        pmxBody6.RotationDamping = 0.5f;
        PmxBodyPassGroup pmxBodyPassGroup6 = new PmxBodyPassGroup();
        pmxBodyPassGroup6.Flags[0] = true;
        pmxBodyPassGroup6.Flags[1] = true;
        pmxBodyPassGroup6.Flags[2] = true;
        pmxBodyPassGroup6.Flags[3] = true;
        pmxBodyPassGroup6.Flags[13] = true;
        pmxBody6.PassGroup = pmxBodyPassGroup6;
        pmxBody6.Mode = PmxBody.ModeType.Static;
        pmxBody6.BoxSize = new Vector3(0.1f, 0.1f, 0.1f);
        this.pmxFile.BodyList.Add(pmxBody6);
        PmxBody pmxBody7 = new PmxBody();
        pmxBody7.Name = "右AH1";
        pmxBody7.Bone = this.sbi("右AH1");
        pmxBody7.Position = this.sb("右AH1").Position;
        pmxBody7.BoxType = PmxBody.BoxKind.Sphere;
        pmxBody7.Group = 13;
        pmxBody7.Mass = 0.4f;
        pmxBody7.PositionDamping = 0.9999f;
        pmxBody7.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup7 = new PmxBodyPassGroup();
        pmxBodyPassGroup7.Flags[0] = true;
        pmxBodyPassGroup7.Flags[1] = true;
        pmxBodyPassGroup7.Flags[2] = true;
        pmxBodyPassGroup7.Flags[3] = true;
        pmxBodyPassGroup7.Flags[13] = true;
        pmxBody7.PassGroup = pmxBodyPassGroup7;
        pmxBody7.Mode = PmxBody.ModeType.Dynamic;
        pmxBody7.BoxSize = new Vector3(0.3f, 0.0f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody7);
        PmxBody pmxBody8 = new PmxBody();
        pmxBody8.Name = "右AH2";
        pmxBody8.Bone = this.sbi("右AH2");
        pmxBody8.Position = this.sb("右AH2").Position;
        pmxBody8.BoxType = PmxBody.BoxKind.Sphere;
        pmxBody8.Group = 13;
        pmxBody8.Mass = 0.1f;
        pmxBody8.PositionDamping = 0.9999f;
        pmxBody8.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup8 = new PmxBodyPassGroup();
        pmxBodyPassGroup8.Flags[0] = true;
        pmxBodyPassGroup8.Flags[1] = true;
        pmxBodyPassGroup8.Flags[2] = true;
        pmxBodyPassGroup8.Flags[3] = true;
        pmxBodyPassGroup8.Flags[13] = true;
        pmxBody8.PassGroup = pmxBodyPassGroup8;
        pmxBody8.Mode = PmxBody.ModeType.Dynamic;
        pmxBody8.BoxSize = new Vector3(0.3f, 0.0f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody8);
        PmxBody pmxBody9 = new PmxBody();
        pmxBody9.Name = "右胸操作接続";
        pmxBody9.Bone = -1;
        pmxBody9.Position = (this.sb("右AH2").Position + this.sb("右AH1").Position) / 2f;
        pmxBody9.BoxType = PmxBody.BoxKind.Capsule;
        pmxBody9.Group = 13;
        pmxBody9.Mass = 0.1f;
        pmxBody9.PositionDamping = 0.9999f;
        pmxBody9.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup9 = new PmxBodyPassGroup();
        pmxBodyPassGroup9.Flags[0] = true;
        pmxBodyPassGroup9.Flags[1] = true;
        pmxBodyPassGroup9.Flags[2] = true;
        pmxBodyPassGroup9.Flags[3] = true;
        pmxBodyPassGroup9.Flags[13] = true;
        pmxBody9.PassGroup = pmxBodyPassGroup9;
        pmxBody9.Mode = PmxBody.ModeType.Dynamic;
        pmxBody9.BoxSize = new Vector3(0.1f, 1.515f, 0.0f);
        pmxBody9.Rotation = new Vector3(0.0f, 1.570796f, -1.570796f);
        this.pmxFile.BodyList.Add(pmxBody9);
        PmxBody pmxBody10 = new PmxBody();
        pmxBody10.Name = "右胸操作衝突";
        pmxBody10.Bone = -1;
        pmxBody10.Position = new Vector3(this.sb("右胸操作").Position.X, this.sb("右胸操作").Position.Y, this.sb("右胸操作").Position.Z - 0.12f);
        pmxBody10.BoxType = PmxBody.BoxKind.Sphere;
        pmxBody10.Group = 7;
        pmxBody10.Mass = 0.1f;
        pmxBody10.PositionDamping = 0.9999f;
        pmxBody10.RotationDamping = 0.99f;
        PmxBodyPassGroup pmxBodyPassGroup10 = new PmxBodyPassGroup();
        pmxBodyPassGroup10.Flags[0] = true;
        pmxBodyPassGroup10.Flags[13] = true;
        pmxBody10.PassGroup = pmxBodyPassGroup10;
        pmxBody10.Mode = PmxBody.ModeType.Dynamic;
        pmxBody10.BoxSize = new Vector3(0.4f, 0.0f, 0.0f);
        this.pmxFile.BodyList.Add(pmxBody10);
        PmxJoint pmxJoint = new PmxJoint();
        pmxJoint.Name = "左胸操作着衣用";
        pmxJoint.Position = (this.sb("左AH2").Position + this.sb("右AH2").Position) / 2f;
        pmxJoint.BodyA = this.sbdi("左AH2");
        pmxJoint.BodyB = this.sbdi("右AH2");
        this.pmxFile.JointList.Add(pmxJoint);
        pmxJoint.Limit_MoveLow = new Vector3(0.0f, 0.0f, 0.0f);
        pmxJoint.Limit_MoveHigh = new Vector3(0.0f, 0.0f, 0.0f);
        pmxJoint.Limit_AngleLow = new Vector3(0.0f, 0.0f, 0.0f);
        pmxJoint.Limit_AngleHigh = new Vector3(0.0f, 0.0f, 0.0f);
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左胸操作着衣用-",
            Position = (this.sb("左AH2").Position + this.sb("右AH2").Position) / 2f,
            BodyB = this.sbdi("左AH2"),
            BodyA = this.sbdi("右AH2")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左胸操作調整用",
            Position = this.sb("左胸操作").Position,
            BodyA = this.sbdi("左胸操作"),
            BodyB = this.sbdi("左胸操作接続"),
            Limit_MoveHigh = new Vector3(0.0f, 0.3f, 0.0f),
            Limit_AngleLow = new Vector3(-0.8726646f, -0.8726646f, -0.8726646f),
            Limit_AngleHigh = new Vector3(0.8726646f, 0.8726646f, 0.8726646f),
            SpConst_Move = new Vector3(0.0f, 20f, 0.0f),
            SpConst_Rotate = new Vector3(100f, 100f, 100f)
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左胸操作接続",
            Position = new Vector3(this.sb("左胸操作").Position.X, this.sb("左胸操作").Position.Y, this.sb("左胸操作").Position.Z - 0.12f),
            BodyA = this.sbdi("左胸操作接続"),
            BodyB = this.sbdi("左胸操作衝突")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左AH1",
            Position = this.sb("左AH1").Position,
            BodyA = this.sbdi("左AH1"),
            BodyB = this.sbdi("左胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左AH2",
            Position = this.sb("左AH2").Position,
            BodyA = this.sbdi("左AH2"),
            BodyB = this.sbdi("左胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左胸操作接続",
            Position = new Vector3(this.sb("左胸操作").Position.X, this.sb("左胸操作").Position.Y, this.sb("左胸操作").Position.Z - 0.12f),
            BodyB = this.sbdi("左胸操作接続"),
            BodyA = this.sbdi("左胸操作衝突")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左AH1",
            Position = this.sb("左AH1").Position,
            BodyB = this.sbdi("左AH1"),
            BodyA = this.sbdi("左胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "左AH2",
            Position = this.sb("左AH2").Position,
            BodyB = this.sbdi("左AH2"),
            BodyA = this.sbdi("左胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右胸操作調整用",
            Position = this.sb("右胸操作").Position,
            BodyA = this.sbdi("右胸操作"),
            BodyB = this.sbdi("右胸操作接続"),
            Limit_MoveHigh = new Vector3(0.0f, 0.3f, 0.0f),
            Limit_AngleLow = new Vector3(-0.8726646f, -0.8726646f, -0.8726646f),
            Limit_AngleHigh = new Vector3(0.8726646f, 0.8726646f, 0.8726646f),
            SpConst_Move = new Vector3(0.0f, 20f, 0.0f),
            SpConst_Rotate = new Vector3(100f, 100f, 100f)
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右胸操作接続",
            Position = new Vector3(this.sb("右胸操作").Position.X, this.sb("右胸操作").Position.Y, this.sb("右胸操作").Position.Z - 0.12f),
            BodyA = this.sbdi("右胸操作接続"),
            BodyB = this.sbdi("右胸操作衝突")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右AH1",
            Position = this.sb("右AH1").Position,
            BodyA = this.sbdi("右AH1"),
            BodyB = this.sbdi("右胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右AH2",
            Position = this.sb("右AH2").Position,
            BodyA = this.sbdi("右AH2"),
            BodyB = this.sbdi("右胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右胸操作接続",
            Position = new Vector3(this.sb("右胸操作").Position.X, this.sb("右胸操作").Position.Y, this.sb("右胸操作").Position.Z - 0.12f),
            BodyB = this.sbdi("右胸操作接続"),
            BodyA = this.sbdi("右胸操作衝突")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右AH1",
            Position = this.sb("右AH1").Position,
            BodyB = this.sbdi("右AH1"),
            BodyA = this.sbdi("右胸操作接続")
        });
        this.pmxFile.JointList.Add(new PmxJoint()
        {
            Name = "右AH2",
            Position = this.sb("右AH2").Position,
            BodyB = this.sbdi("右AH2"),
            BodyA = this.sbdi("右胸操作接続")
        });
    }

    public void vasicphy()
    {
        Transform transform = GameObject.Find("BodyTop").transform;
        CapsuleCollider[] componentsInChildren1 = ((Component)transform).GetComponentsInChildren<CapsuleCollider>();
        for (int index = 0; index < componentsInChildren1.Length; ++index)
            this.pmxFile.BodyList.Add(new PmxBody()
            {
                Name = (componentsInChildren1[index]).name,
                Bone = this.sbi((componentsInChildren1[index]).name),
                Position = new Vector3((float)-((Component)componentsInChildren1[index]).transform.position.x, (float)((Component)componentsInChildren1[index]).transform.position.y, (float)-((Component)componentsInChildren1[index]).transform.position.z) * (float)this.scale,
                Rotation = new Vector3((float)-((Component)componentsInChildren1[index]).transform.rotation.x, (float)((Component)componentsInChildren1[index]).transform.rotation.y, (float)-((Component)componentsInChildren1[index]).transform.rotation.z),
                BoxSize = new Vector3((float)((double)componentsInChildren1[index].radius * (double)this.scale / 2.0), (float)((double)componentsInChildren1[index].height * (double)this.scale / 3.0 * 2.0), 0.0f),
                BoxType = PmxBody.BoxKind.Capsule,
                Mode = PmxBody.ModeType.Static,
                Group = 13,
                Mass = 0.1f,
                PositionDamping = 0.9999f,
                RotationDamping = 0.99f
            });
        SphereCollider[] componentsInChildren2 = ((Component)transform).GetComponentsInChildren<SphereCollider>();
        for (int index = 0; index < componentsInChildren2.Length; ++index)
            this.pmxFile.BodyList.Add(new PmxBody()
            {
                Name = (componentsInChildren2[index]).name,
                Bone = this.sbi((componentsInChildren2[index]).name),
                Position = new Vector3((float)-((Component)componentsInChildren2[index]).transform.position.x, (float)((Component)componentsInChildren2[index]).transform.position.y, (float)-((Component)componentsInChildren2[index]).transform.position.z) * (float)this.scale,
                Rotation = new Vector3((float)-((Component)componentsInChildren2[index]).transform.rotation.x, (float)((Component)componentsInChildren2[index]).transform.rotation.y, (float)-((Component)componentsInChildren2[index]).transform.rotation.z),
                BoxSize = new Vector3(componentsInChildren2[index].radius, 0.0f, 0.0f) * (float)this.scale / 3f * 2f,
                BoxType = PmxBody.BoxKind.Sphere,
                Mode = PmxBody.ModeType.Static,
                Group = 13,
                Mass = 0.1f,
                PositionDamping = 0.9999f,
                RotationDamping = 0.99f
            });
    }

    public void addnode()
    {
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "センター",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("センター"))
      }
        });
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "ＩＫ",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("左足ＩＫ")),
        PmxNode.NodeElement.BoneElement(this.sbi("左つま先ＩＫ")),
        PmxNode.NodeElement.BoneElement(this.sbi("右足ＩＫ")),
        PmxNode.NodeElement.BoneElement(this.sbi("右つま先ＩＫ"))
      }
        });
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "体(上)",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("上半身")),
        PmxNode.NodeElement.BoneElement(this.sbi("上半身2")),
        PmxNode.NodeElement.BoneElement(this.sbi("首")),
        PmxNode.NodeElement.BoneElement(this.sbi("頭")),
        PmxNode.NodeElement.BoneElement(this.sbi("両目")),
        PmxNode.NodeElement.BoneElement(this.sbi("右目")),
        PmxNode.NodeElement.BoneElement(this.sbi("左目"))
      }
        });
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "腕",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("左肩")),
        PmxNode.NodeElement.BoneElement(this.sbi("左腕")),
        PmxNode.NodeElement.BoneElement(this.sbi("左腕捩")),
        PmxNode.NodeElement.BoneElement(this.sbi("左ひじ")),
        PmxNode.NodeElement.BoneElement(this.sbi("左手捩")),
        PmxNode.NodeElement.BoneElement(this.sbi("左手首")),
        PmxNode.NodeElement.BoneElement(this.sbi("右肩")),
        PmxNode.NodeElement.BoneElement(this.sbi("右腕")),
        PmxNode.NodeElement.BoneElement(this.sbi("右腕捩")),
        PmxNode.NodeElement.BoneElement(this.sbi("右ひじ")),
        PmxNode.NodeElement.BoneElement(this.sbi("右手捩")),
        PmxNode.NodeElement.BoneElement(this.sbi("右手首"))
      }
        });
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "指",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("左薬指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("左薬指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("左薬指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("左親指０")),
        PmxNode.NodeElement.BoneElement(this.sbi("左親指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("左親指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("左中指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("左中指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("左中指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("左小指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("左小指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("左小指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("左人指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("左人指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("左人指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("右薬指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("右薬指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("右薬指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("右親指０")),
        PmxNode.NodeElement.BoneElement(this.sbi("右親指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("右親指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("右中指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("右中指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("右中指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("右小指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("右小指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("右小指３")),
        PmxNode.NodeElement.BoneElement(this.sbi("右人指１")),
        PmxNode.NodeElement.BoneElement(this.sbi("右人指２")),
        PmxNode.NodeElement.BoneElement(this.sbi("右人指３"))
      }
        });
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "体(下)",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("下半身"))
      }
        });
        this.pmxFile.NodeList.Add(new PmxNode()
        {
            Name = "足",
            ElementList = {
        PmxNode.NodeElement.BoneElement(this.sbi("左足")),
        PmxNode.NodeElement.BoneElement(this.sbi("左ひざ")),
        PmxNode.NodeElement.BoneElement(this.sbi("左足首")),
        PmxNode.NodeElement.BoneElement(this.sbi("右足")),
        PmxNode.NodeElement.BoneElement(this.sbi("右ひざ")),
        PmxNode.NodeElement.BoneElement(this.sbi("右足首"))
      }
        });
    }
}
