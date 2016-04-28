using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace PlatformerPro
{
	/// <summary>
	/// Proximity trigger acts similar to a trigger in Unity but is more easily hooked in to the
	/// platformer system.
	/// </summary>
	public class ProximityTrigger : Trigger
	{

		/// <summary>
		/// The proximity radius.
		/// </summary>
		public float radius;

		/// <summary>
		/// Characters in proximity.
		/// </summary>
		protected List<Character> charactersInProximity;

		/// <summary>
		/// Unity update hook.
		/// </summary>
		void Update ()
		{
			CheckProximity();
		}

		/// <summary>
		/// Initialise the sensor.
		/// </summary>
		override protected void Init()
		{
			base.Init();
			charactersInProximity = new List<Character>();
		}

        private void EnemiesExist(int sectionId)
        {
            int enemyCount = 0;
            int totalEnemieCount = 0;

            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

            foreach (Enemy enemy in enemies)
            {
                if (enemy.levelSection == 1)
                {
                    enemyCount++;
                }
                totalEnemieCount++;
            }

            if(enemyCount == 0)
            {
                EnterTrigger(GameObject.FindObjectOfType<Character>());
            }
            if(enemies.Length ==0)
            {
                //What scence am i on?
                LevelManager.PreviousLevel = SceneManager.GetActiveScene().name;
             //   LevelManager.pre
              //  SceneManager.LoadScene(supportingString);
            }

            /*
            if (enemyCount == 0)
            {
                if (allCharacters != null)
                {
                    for (int i = 0; i < allCharacters.Count; i++)
                    {
                        EnterTrigger(allCharacters[i]);

                    }
                }
            }
            */

            Debug.Log("Enemies = " + enemyCount);
        }


        /// <summary>
        /// Checks for characters in range.
        /// </summary>
        virtual protected void CheckProximity()
		{
            /*
			if (allCharacters != null)
			{
				for (int i = 0; i < allCharacters.Count; i++)
				{
					if (allCharacters[i] != null)
					{
						if (Vector2.Distance(transform.position, allCharacters[i].transform.position) < radius)
						{
							if (charactersInProximity.Contains (allCharacters[i]))
							{
								// We could send a trigger "stay" here
							}
							else
							{
								EnterTrigger(allCharacters[i]);
								charactersInProximity.Add (allCharacters[i]);
							}
						}
						else
						{
							if (charactersInProximity.Contains (allCharacters[i]))
							{
								if (autoLeaveTime == 0 ) LeaveTrigger(allCharacters[i]);
								charactersInProximity.Remove (allCharacters[i]);
							}
						}
					}
				}
			}
            */
            EnemiesExist(1);
		}


#if UNITY_EDITOR

		/// <summary>
		/// Unity gizmo hook, draw the radius.
		/// </summary>
		void OnDrawGizmos()
		{
			UnityEditor.Handles.color = Trigger.GizmoColor;
			UnityEditor.Handles.DrawSolidDisc(transform.position, new Vector3(0,0,1), radius);
			if (receivers != null)
			{
				Gizmos.color = Trigger.GizmoColor;
				foreach (TriggerTarget receiver in receivers) 
				{
					if (receiver != null && receiver.receiver != null)
					{
						Vector2 direction = (receiver.receiver.transform.position - transform.position).normalized;
						Gizmos.DrawLine(transform.position + (Vector3)(direction * radius), receiver.receiver.transform.position);
					}
				}
			}
		}

#endif

	}




}