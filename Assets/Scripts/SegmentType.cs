using UnityEngine;

namespace AssemblyCSharp
{
	public class SegmentType
	{
		// Type of bent:
		// 1 = left.
		// 2 = right.
		// Everything else = straight.
		public int bentType { get; set; }

		// Type of slope:
		// 1 = slope.
		// Everything else = straight.
		public int slopeType { get; set; }

		public SegmentType() {
			slopeType = 0;
			bentType = 0;
		}

		public void randomize(){
			bentType = Random.Range (0, 10);
			slopeType = Random.Range (0, 10);
		}
	}

}

