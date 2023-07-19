using BepInEx;
using RoR2;
using RoR2.CharacterAI;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TweakXiConstructs
{
  [BepInPlugin("com.Nuxlar.TweakXiConstructs", "TweakXiConstructs", "1.0.0")]

  public class TweakXiConstructs : BaseUnityPlugin
  {
    private GameObject xiMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MegaConstructMaster.prefab").WaitForCompletion();
    private GameObject xiBody = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MegaConstructBody.prefab").WaitForCompletion();

    public void Awake()
    {
      XiBodyChanges();
      XiMasterChanges();
    }

    private void XiBodyChanges()
    {
      CharacterBody body = xiBody.GetComponent<CharacterBody>();
      body.baseMoveSpeed = 12;
    }

    private void XiMasterChanges()
    {
      AISkillDriver fleeDriver = xiMaster.GetComponents<AISkillDriver>().Where<AISkillDriver>(x => x.customName == "FleeStep").First<AISkillDriver>();
      AISkillDriver stopDriver = xiMaster.GetComponents<AISkillDriver>().Where<AISkillDriver>(x => x.customName == "StopStep").First<AISkillDriver>();
      AISkillDriver followDriver = xiMaster.GetComponents<AISkillDriver>().Where<AISkillDriver>(x => x.customName == "FollowStep").First<AISkillDriver>();
      AISkillDriver fastDriver = xiMaster.GetComponents<AISkillDriver>().Where<AISkillDriver>(x => x.customName == "FollowFast").First<AISkillDriver>();
      AISkillDriver shootDriver = xiMaster.GetComponents<AISkillDriver>().Where<AISkillDriver>(x => x.customName == "ShootStep").First<AISkillDriver>();
      Destroy(stopDriver);

      fleeDriver.minDistance = 0;
      fleeDriver.ignoreNodeGraph = false;
      followDriver.driverUpdateTimerOverride = -1;
      followDriver.minDistance = 50;
      followDriver.noRepeat = false;
      fastDriver.driverUpdateTimerOverride = -1;
      fastDriver.shouldSprint = true;
      shootDriver.maxDistance = 75;
    }
  }
}