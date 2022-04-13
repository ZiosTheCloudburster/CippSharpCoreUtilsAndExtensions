using UnityEngine;

namespace CippSharp.Core.Extensions
{
	public static class ConfigurableJointExtensions
	{
		#region Motion
		
		/// <summary>
		/// Set the joint motions to new values
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public static void SetMotions(this ConfigurableJoint joint, ConfigurableJointMotion x, ConfigurableJointMotion y, ConfigurableJointMotion z)
		{
			joint.SetMotionX(x);
			joint.SetMotionY(y);
			joint.SetMotionZ(z);
		}

		/// <summary>
		/// Set joint's xMotion <see cref="ConfigurableJoint"/>
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="motion"></param>
		public static void SetMotionX(this ConfigurableJoint joint, ConfigurableJointMotion motion)
		{
			joint.xMotion = motion;
		}

		/// <summary>
		/// Set joint's yMotion <see cref="ConfigurableJoint"/>
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="motion"></param>
		public static void SetMotionY(this ConfigurableJoint joint, ConfigurableJointMotion motion)
		{
			joint.yMotion = motion;
		}

		/// <summary>
		/// Set joint's xMotion <see cref="ConfigurableJoint"/>
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="motion"></param>
		public static void SetMotionZ(this ConfigurableJoint joint, ConfigurableJointMotion motion)
		{
			joint.zMotion = motion;
		}

		#endregion

		#region Angular Motion

		/// <summary>
		/// Set the joint angular motions to new values
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public static void SetAngularMotions(this ConfigurableJoint joint, ConfigurableJointMotion x, ConfigurableJointMotion y, ConfigurableJointMotion z)
		{
			joint.SetAngularMotionX(x);
			joint.SetAngularMotionY(y);
			joint.SetAngularMotionZ(z);
		}

		/// <summary>
		/// Set joint's angularXMotion <see cref="ConfigurableJoint"/>
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="angularMotion"></param>
		public static void SetAngularMotionX(this ConfigurableJoint joint, ConfigurableJointMotion angularMotion)
		{
			joint.angularXMotion = angularMotion;
		}
		
		/// <summary>
		/// Set joint's angularYMotion <see cref="ConfigurableJoint"/>
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="angularMotion"></param>
		public static void SetAngularMotionY(this ConfigurableJoint joint, ConfigurableJointMotion angularMotion)
		{
			joint.angularYMotion = angularMotion;
		}

		/// <summary>
		/// Set joint's angularZMotion <see cref="ConfigurableJoint"/>
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="angularMotion"></param>
		public static void SetAngularMotionZ(this ConfigurableJoint joint, ConfigurableJointMotion angularMotion)
		{
			joint.angularZMotion = angularMotion;
		}

		#endregion

		#region Linear Limit

		/// <summary>
		/// Set linear limit distance of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="distance"></param>
		public static void SetLinearLimitDistance(this ConfigurableJoint joint, float distance)
		{
			SoftJointLimit limit = joint.linearLimit;
			limit.limit = distance;
			joint.linearLimit = limit;
		}

		/// <summary>
		/// Set linear limit by copying values from a new limit.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newLimit"></param>
		public static void SetLinearLimit(this ConfigurableJoint joint, SoftJointLimit newLimit)
		{
			SoftJointLimit limit = joint.linearLimit;
			
			limit.bounciness = newLimit.bounciness;
			limit.contactDistance = newLimit.contactDistance;
			limit.limit = newLimit.limit;

			joint.linearLimit = limit;
		}

		#endregion

		#region Linear Spring

		/// <summary>
		/// Set linear spring damper of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="damper"></param>
		public static void SetLinearSpringDamper(this ConfigurableJoint joint, float damper)
		{
			SoftJointLimitSpring spring = joint.linearLimitSpring;
			spring.damper = damper;
			joint.linearLimitSpring = spring;
		}

		/// <summary>
		/// Set linear limit spring by copying values from a new limit spring.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newLimitSpring"></param>
		public static void SetLinearSpring(this ConfigurableJoint joint, SoftJointLimitSpring newLimitSpring)
		{
			SoftJointLimitSpring spring = joint.linearLimitSpring;

			spring.damper = newLimitSpring.damper;
			spring.spring = newLimitSpring.spring;

			joint.linearLimitSpring = spring;
		}

		#endregion

		#region Angular Limit

			#region X Limit

			/// <summary>
			/// Set the limit angle for highAngularXLimit of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="angle"></param>
			public static void SetHighAngularXLimitAngle(this ConfigurableJoint joint, float angle)
			{
				SoftJointLimit limit = joint.highAngularXLimit;

				limit.limit = angle;

				joint.highAngularXLimit = limit;
			}

			/// <summary>
			/// Set the limit angle for highAngularXLimit of joint by copying values from a new limit.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newLimit"></param>
			public static void SetHighAngularXLimit(this ConfigurableJoint joint, SoftJointLimit newLimit)
			{
				SoftJointLimit limit = joint.highAngularXLimit;

				limit.bounciness = newLimit.bounciness;
				limit.contactDistance = newLimit.contactDistance;
				limit.limit = newLimit.limit;

				joint.highAngularXLimit = limit;
			}

			/// <summary>
			/// Set the limit angle for lowAngularXLimit of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="angle"></param>
			public static void SetLowAngularXLimitAngle(this ConfigurableJoint joint, float angle)
			{
				SoftJointLimit limit = joint.lowAngularXLimit;

				limit.limit = angle;

				joint.lowAngularXLimit = limit;
			}

			/// <summary>
			/// Set the limit angle for lowAngularXLimit of joint by copying values from a new limit.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newLimit"></param>
			public static void SetLowAngularXLimit(this ConfigurableJoint joint, SoftJointLimit newLimit)
			{
				SoftJointLimit limit = joint.lowAngularXLimit;

				limit.bounciness = newLimit.bounciness;
				limit.contactDistance = newLimit.contactDistance;
				limit.limit = newLimit.limit;

				joint.lowAngularXLimit = limit;
			}
			
			#endregion
		
		/// <summary>
		/// Set the limit angle for angularYLimit of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="angle"></param>
		public static void SetAngularYLimitAngle(this ConfigurableJoint joint, float angle)
		{
			SoftJointLimit limit = joint.angularYLimit;

			limit.limit = angle;

			joint.angularYLimit = limit;
		}

		/// <summary>
		/// Set the limit angle for angularYLimit of joint by copying values from a new limit.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newLimit"></param>
		public static void SetAngularYLimit(this ConfigurableJoint joint, SoftJointLimit newLimit)
		{
			SoftJointLimit limit = joint.angularYLimit;

			limit.bounciness = newLimit.bounciness;
			limit.contactDistance = newLimit.contactDistance;
			limit.limit = newLimit.limit;

			joint.angularZLimit = limit;
		}

		/// <summary>
		/// Set the limit angle for angularZLimit of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="angle"></param>
		public static void SetAngularZLimitAngle(this ConfigurableJoint joint, float angle)
		{
			SoftJointLimit limit = joint.angularZLimit;

			limit.limit = angle;

			joint.angularZLimit = limit;
		}

		/// <summary>
		/// Set the limit angle for angularZLimit of joint by copying values from a new limit.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newLimit"></param>
		public static void SetAngularZLimit(this ConfigurableJoint joint, SoftJointLimit newLimit)
		{
			SoftJointLimit limit = joint.angularZLimit;

			limit.bounciness = newLimit.bounciness;
			limit.contactDistance = newLimit.contactDistance;
			limit.limit = newLimit.limit;

			joint.angularZLimit = limit;
		}

		#endregion

		#region Drive

			#region X Drive
			
			/// <summary>
			/// Set spring of x drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newSpring"></param>
			public static void SetDriveXSpring(this ConfigurableJoint joint, float newSpring)
			{
				JointDrive drive = joint.xDrive;
				drive.positionSpring = newSpring;
				joint.xDrive = drive;
			}

			/// <summary>
			/// Set damp of x drive of joint
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newDamp"></param>
			public static void SetDriveXDamp(this ConfigurableJoint joint, float newDamp)
			{
				JointDrive drive = joint.xDrive;
				drive.positionDamper = newDamp;
				joint.xDrive = drive;
			}

			/// <summary>
			/// Set drive x by copying his values from a new joint drive.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newJointDrive"></param>
			public static void SetDriveX(this ConfigurableJoint joint, JointDrive newJointDrive)
			{
				JointDrive drive = joint.xDrive;

				drive.maximumForce = newJointDrive.maximumForce;
				drive.positionDamper = newJointDrive.positionDamper;
				drive.positionSpring = newJointDrive.positionSpring;

				joint.xDrive = drive;
			}
			
			#endregion

			#region Y Drive

			/// <summary>
			/// Set spring of y drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newSpring"></param>
			public static void SetDriveYSpring(this ConfigurableJoint joint, float newSpring)
			{
				JointDrive drive = joint.yDrive;
				drive.positionSpring = newSpring;
				joint.yDrive = drive;
			}
			
			/// <summary>
			/// Set damp of y drive of joint
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newDamp"></param>
			public static void SetDriveYDamp(this ConfigurableJoint joint, float newDamp)
			{
				JointDrive drive = joint.yDrive;
				drive.positionDamper = newDamp;
				joint.yDrive = drive;
			}

			/// <summary>
			/// Set drive y by copying his values from a new joint drive.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newJointDrive"></param>
			public static void SetDriveY(this ConfigurableJoint joint, JointDrive newJointDrive)
			{
				JointDrive drive = joint.yDrive;
				
				drive.maximumForce = newJointDrive.maximumForce;
				drive.positionDamper = newJointDrive.positionDamper;
				drive.positionSpring = newJointDrive.positionSpring;

				joint.yDrive = drive;
			}

			#endregion

			#region Z Drive
			
			/// <summary>
			/// Set spring of z drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newSpring"></param>
			public static void SetZSpring(this ConfigurableJoint joint, float newSpring)
			{
				JointDrive drive = joint.zDrive;
				drive.positionSpring = newSpring;
				joint.zDrive = drive;
			}

			/// <summary>
			/// Set damp of z drive of joint
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newDamp"></param>
			public static void SetDriveZDamp(this ConfigurableJoint joint, float newDamp)
			{
				JointDrive drive = joint.zDrive;
				drive.positionDamper = newDamp;
				joint.zDrive = drive;
			}

			/// <summary>
			/// Set drive z by copying his values from a new joint drive.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newJointDrive"></param>
			public static void SetDriveZ(this ConfigurableJoint joint, JointDrive newJointDrive)
			{
				JointDrive drive = joint.zDrive;
						
				drive.maximumForce = newJointDrive.maximumForce;
				drive.positionDamper = newJointDrive.positionDamper;
				drive.positionSpring = newJointDrive.positionSpring;

				joint.zDrive = drive;
			}

			#endregion

		#endregion

		#region Angular Drive

			#region X Angular Drive
			
			/// <summary>
			/// Set spring of x angular drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newSpring"></param>
			public static void SetAngularDriveXSpring(this ConfigurableJoint joint, float newSpring)
			{
				JointDrive drive = joint.angularXDrive;

				drive.positionSpring = newSpring;

				joint.angularXDrive = drive;
			}
			
			/// <summary>
			/// Set damp of x angular drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newDamp"></param>
			public static void SetAngularDriveXDamp(this ConfigurableJoint joint, float newDamp)
			{
				JointDrive drive = joint.angularXDrive;

				drive.positionDamper = newDamp;

				joint.angularXDrive = drive;
			}
			
			/// <summary>
			/// Set angular drive x by copying his values from a new joint drive.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newJointDrive"></param>
			public static void SetAngularDriveX(this ConfigurableJoint joint, JointDrive newJointDrive)
			{
				JointDrive drive = joint.angularXDrive;

				drive.maximumForce = newJointDrive.maximumForce;
				drive.positionDamper = newJointDrive.positionDamper;
				drive.positionSpring = newJointDrive.positionSpring;
				
				joint.angularXDrive = drive;
			}

			#endregion

			#region YZ Angular Drive

			/// <summary>
			/// Set spring of yz angular drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newSpring"></param>
			public static void SetAngularDriveYZSpring(this ConfigurableJoint joint, float newSpring)
			{
				JointDrive drive = joint.angularXDrive;

				drive.positionSpring = newSpring;

				joint.angularYZDrive = drive;
			}

			/// <summary>
			/// Set damp of yz angular drive of joint.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newDamp"></param>
			public static void SetAngularDriveYZDamp(this ConfigurableJoint joint, float newDamp)
			{
				JointDrive drive = joint.angularXDrive;

				drive.positionDamper = newDamp;

				joint.angularYZDrive = drive;
			}
			
			/// <summary>
			/// Set angular drive yz by copying his values from a new joint drive.
			/// </summary>
			/// <param name="joint"></param>
			/// <param name="newJointDrive"></param>
			public static void SetAngularDriveYZ(this ConfigurableJoint joint, JointDrive newJointDrive)
			{
				JointDrive drive = joint.angularYZDrive;

				drive.maximumForce = newJointDrive.maximumForce;
				drive.positionDamper = newJointDrive.positionDamper;
				drive.positionSpring = newJointDrive.positionSpring;

				joint.angularYZDrive = drive;
			}

			#endregion
	
		#endregion

		#region Anchor

		/// <summary>
		/// Set anchor of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newAnchor"></param>
		public static void SetAnchor(this ConfigurableJoint joint, Vector3 newAnchor)
		{
			joint.anchor = newAnchor;
		}

		/// <summary>
		/// Set anchor x value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newX"></param>
		public static void SetAnchorX(this ConfigurableJoint joint, float newX)
		{
			Vector3 newAnchor = joint.anchor;
			newAnchor.x = newX;
			joint.SetAnchor(newAnchor);
		}

		/// <summary>
		/// Set anchor y value of joint
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newY"></param>
		public static void SetAnchorY(this ConfigurableJoint joint, float newY)
		{
			Vector3 newAnchor = joint.anchor;
			newAnchor.y = newY;
			joint.SetAnchor(newAnchor);
		}

		/// <summary>
		/// Set anchor z value of joint
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newZ"></param>
		public static void SetAnchorZ(this ConfigurableJoint joint, float newZ)
		{
			Vector3 newAnchor = joint.anchor;
			newAnchor.z = newZ;
			joint.SetAnchor(newAnchor);
		}

		#endregion

		#region Connected Anchor

		/// <summary>
		/// Set connected anchor of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newConnectedAnchor"></param>
		public static void SetConnectedAnchor(this ConfigurableJoint joint, Vector3 newConnectedAnchor)
		{
			joint.connectedAnchor = newConnectedAnchor;
		}

		/// <summary>
		/// Set connected anchor x value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newX"></param>
		public static void SetConnectedAnchorX(this ConfigurableJoint joint, float newX)
		{
			Vector3 newConnectedAnchor = joint.connectedAnchor;
			newConnectedAnchor.x = newX;
			joint.SetConnectedAnchor(newConnectedAnchor);
		}

		/// <summary>
		/// Set connected anchor y value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newY"></param>
		public static void SetConnectedAnchorY(this ConfigurableJoint joint, float newY)
		{
			Vector3 newConnectedAnchor = joint.connectedAnchor;
			newConnectedAnchor.y = newY;
			joint.SetConnectedAnchor(newConnectedAnchor);
		}

		/// <summary>
		/// Set connected anchor z value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newZ"></param>
		public static void SetConnectedAnchorZ(this ConfigurableJoint joint, float newZ)
		{
			Vector3 newConnectedAnchor = joint.connectedAnchor;
			newConnectedAnchor.z = newZ;
			joint.SetConnectedAnchor(newConnectedAnchor);
		}

		#endregion

		#region Target Position

		/// <summary>
		/// Set target position of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newTargetPostiion"></param>
		public static void SetTargetPosition(this ConfigurableJoint joint, Vector3 newTargetPostiion)
		{
			joint.targetPosition = newTargetPostiion;
		}

		/// <summary>
		/// Set target position x value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newX"></param>
		public static void SetTargetPositionX(this ConfigurableJoint joint, float newX)
		{
			Vector3 newTargetPosition = joint.targetPosition;
			newTargetPosition.x = newX;
			joint.targetPosition = newTargetPosition;
		}

		/// <summary>
		/// Set target position y value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newY"></param>
		public static void SetTargetPositionY(this ConfigurableJoint joint, float newY)
		{
			Vector3 newTargetPosition = joint.targetPosition;
			newTargetPosition.y = newY;
			joint.targetPosition = newTargetPosition;
		}

		/// <summary>
		/// Set target position z value of joint.
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="newZ"></param>
		public static void SetTargetPositionZ(this ConfigurableJoint joint, float newZ)
		{
			Vector3 newTargetPosition = joint.targetPosition;
			newTargetPosition.z = newZ;
			joint.targetPosition = newTargetPosition;
		}

		#endregion

	}
}
